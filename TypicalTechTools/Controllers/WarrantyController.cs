using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace TypicalTechTools.Controllers
{
    public class WarrantyController : Controller
    {
        private IWebHostEnvironment Environment;

        public WarrantyController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        public ActionResult Index()
        {
            //Retrieve a list of the currently uploaded files and put it in the Viewbag to be passed to the view when opened.
            ViewBag.FileList = GetUploadFileList();
            return View();
        }

        public ActionResult Upload(IFormFile file)
        {
            if (file == null)
            {
                //generate an error message to infrom the user they have no file selected.
                ViewBag.WarrantyErrorMessage = "No File Selected";
                //Retrieve a list of the currently uploaded files and put it in the Viewbag to be passed to the view when opened.
                ViewBag.FileList = GetUploadFileList();
                return View(nameof(Index));
            }

            //Check to see if there already is a file in the system with the same name.
            string fileName = GenerateUniqueFileName(file.FileName);
            //CReate array to store fiel contents.
            byte[] fileContents;
            //Use a memory stream to convert the file to a byte array.
            //The using statement manages and disposes of the stream when needed.
            using (MemoryStream stream = new MemoryStream())
            {
                //Copy the file into the file stream
                file.CopyToAsync(stream);
                //COnvert the file contents into a byte array
                fileContents = stream.ToArray();
            }
            //Using statement to manage and dispose of our class which implements
            //the management of the byte array in memory.
            using (MemoryStream encDataStream = new MemoryStream(fileContents))
            {
                //Combine the file name and upload path to get the absolute path name for the file.
                var targetFile = Path.Combine(this.Environment.WebRootPath,"Uploads\\" + fileName);
                //Using statement to manage and dispose of our class which implements
                //and manages the file writing process.
                using (var fileStream = new FileStream(targetFile, FileMode.Create))
                {
                    //Write the file to the upload directory.
                    encDataStream.WriteTo(fileStream);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DownloadClaimForm()
        {
            //Get the file path of the warranty form.
            string filePath = Path.Combine(this.Environment.WebRootPath, "Forms\\TypicalTools_WarrantyForm.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", Path.GetFileName(filePath));
        }

        public ActionResult DownloadFile(string filePath) 
        {
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", Path.GetFileName(filePath));
        }

        public ActionResult Delete(string filePath) 
        {
            //Creates an anonymous object list where each entry holds 2 values, the filename and the filepath.
            ViewBag.File = new { Name = Path.GetFileName(filePath), Path = filePath };    
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string filePath)
        {
            //Try catch to handl if the file delete has any issues
            try
            {
                //Tell the system to delete the file based upon the provided filepath.
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return RedirectToAction("Index");
        }


        private IEnumerable<UploadedFileDetails> GetUploadFileList()
        {
            //Retrieves all the current files located in the Uploads folder of the wwwroot directory.
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "Uploads\\"));
            //Creates an list of UploadFileDetails objects to hold the filename and the filepath of each file in the folder.
            var files = filePaths.Select(file => new UploadedFileDetails{ Name = Path.GetFileName(file), Path = file });
            //Return the list to the caller.
            return files;
        }

        /// <summary>
        /// Takes a provided file name and checks it against the current files in the uploads folder.
        /// If a file exists with the same name, the method will try to append the file name with a numeric
        /// value and then check if that updated file name exists. it will keep going until a unique name 
        /// is determined.
        /// </summary>
        /// <param name="fileName">The starting file name of the file(Example: File.txt)</param>
        /// <returns>
        /// The original name if no match is found(File.txt) or a modified file name generated using the
        /// original file name(File(1).txt etc)
        /// </returns>
        private string GenerateUniqueFileName(string fileName)
        {
            //Takes the statring file name and separates the name and extension sections.
            string startingName = fileName.Split('.')[0];
            string fileExt = fileName.Split('.')[1];
            //Sets the initial updatedFileName to be the same as the starting name. This will be changed if this name
            //is not found to be unique.
            string updatedFileName = startingName;

            //Retrieves all the current files located in the Uploads folder of the wwwroot directory.
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "Uploads\\"));
            //Sets a counter to be used for the numeric modifier for the file name if needed.
            int counter = 1;

            //Checks if any current files in the folder match the file name, if a match is found the loop will run to
            //modify the name and try again.
            while(filePaths.Any(file => Path.GetFileName(file).Split('.')[0].Equals(updatedFileName)))
            {
                //Change the updated name to be the starting name plus a numeric modifier based upon the counter value
                updatedFileName = $"{startingName}({counter})";
                //Increase the counter for if it needs to be higher in the next loop.
                counter++;
            }
            
            //Return the updated file name after it is found to be unique and add the extension back to the name.
            return $"{updatedFileName}.{fileExt}";
        }
    }

    public class UploadedFileDetails
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
}
