using Ghostscript.NET;
using Ghostscript.NET.Processor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CompressApp
{
    public class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        

        public static void Main(string[] pdfilepath)
        {
           
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);

            // Show
            //ShowWindow(handle, SW_SHOW);

            //start dummy path
            List<string> stringPath = new List<string>();
            stringPath.Add(@"C:\Users\ewaty\Desktop\PDFCompressor\COMTest.pdf");
            pdfilepath = stringPath.ToArray();

           
            //end of dummy path

            //SaveToTextFile(pdfilepath);
            ProcessFiles();
        }

        //save paths to text file
        public static void SaveToTextFile(string[] pdfilepath)
        {
            string textFilepath = @"C:\Users\ewaty\Desktop\PDFCompressor\Sample.txt";
            List<string> stringPaths = new List<string>();
            stringPaths.Add(textFilepath);
            string[] pdfilepaths = stringPaths.ToArray();

            foreach (var t in pdfilepath)
            {
                    //checking if file exists in file
                    if (System.IO.File.Exists(textFilepath))
                    {
                        var filename = Path.GetFileName(textFilepath);
                        //writting content to file
                        WriteToFile(t);
                        //reading content to file
                        /*
                        string textfile = System.IO.File.ReadAllText(t);
                        System.Console.WriteLine(textfile);*/

                    }
                    else
                    {
                        Console.WriteLine("File path Not Found");
                    }
               
            }
        }

        public static void WriteToFile(string t)
        {
            string textfilepathsvar = @"C:\Users\ewaty\Desktop\PDFCompressor\Sample.txt";
            string text = t;
            if (new FileInfo(textfilepathsvar).Length > 0)
            {
                // empty
                File.AppendAllText(textfilepathsvar, Environment.NewLine + text);
            }
            /*
            if (new FileInfo(textfilepathsvar).Length != 0)
            {
                // empty
                return;
            }*/


        }

        public static void ProcessFiles()
        {
            string[] pdfilepath;
            List<string> FilePaths = new List<string>();
            FilePaths.Add(@"C:\Users\ewaty\Desktop\PDFCompressor\Sample.txt");
            pdfilepath = FilePaths.ToArray();
            string savePath;
            foreach (var t in pdfilepath)
            {


                // Show
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_SHOW);
                foreach (var rd in t)
                {
                    Console.WriteLine(rd);
                    string[] pdfapthsstored = System.IO.File.ReadAllLines(t);
                    foreach (string value in pdfapthsstored)
                    {
                        // Step 3: access the enumeration variable.
                        Console.WriteLine("FOREACH ITEM: " + value);

                        var p = Path.GetDirectoryName(value);
                        Console.WriteLine(p);
                        savePath = p;


                        string inputstring;
                        string folderName = "";
                        string NewFolderLoc = @"C:\Users\ewaty\source\repos\PDFCompressor\CompressApp\bin\Debug";
                        //get directory with all scanned pdf files
                        List<string> pdffiles = new List<string>();
                        pdffiles = Directory.GetFiles(savePath).Select(d => Path.GetFileName(d)).ToList();
                        if (pdffiles.Count != 0)
                        {
                            foreach (var r in pdffiles)
                            {
                                //foreach file in directory confirm is all files are pdfs
                                string ext = Path.GetExtension(r);
                                if (ext == ".pdf" || ext == ".PDF")
                                {
                                    //get pdf name
                                    string inputstringfull = r;
                                    inputstring = r;


                                    int fileExtPos = inputstring.LastIndexOf(".");
                                    if (fileExtPos >= 0)
                                        inputstring = inputstring.Substring(0, fileExtPos);

                                    //byte[] inputFile = File.ReadAllBytes(folderName + "\\" + r);
                                    //start compression
                                    // Use Path class to manipulate file and directory paths.
                                    string sourceFile = System.IO.Path.Combine(savePath, inputstringfull);
                                    string destFile = System.IO.Path.Combine(NewFolderLoc, inputstringfull);


                                    if (System.IO.Directory.Exists(NewFolderLoc))
                                    {
                                        System.IO.File.Copy(sourceFile, destFile, true);
                                    }
                                    if (!System.IO.Directory.Exists(NewFolderLoc))
                                    {
                                        System.IO.Directory.CreateDirectory(NewFolderLoc);
                                        System.IO.File.Copy(sourceFile, destFile, true);
                                    }



                                    //rename file to be compressed
                                    var originalfilename = inputstringfull;
                                    string newdestFile4 = System.IO.Path.Combine(NewFolderLoc, "Incommingfile.pdf");
                                    string oldcompressedfilepath = System.IO.Path.Combine(NewFolderLoc, originalfilename);

                                    ChangeFilename(oldcompressedfilepath, newdestFile4);


                                    var Outputfile3Compressed = Path.GetFileName(newdestFile4);
                                    var Outputfile3 = "compressed-" + Outputfile3Compressed;
                                    var Inputfile3 = Outputfile3Compressed;


                                    string pathtobatfile = @"C:\Users\ewaty\source\repos\PDFCompressor\CompressApp\bin\Debug\pdftestfile.bat";
                                    var process = new Process();
                                    var startinfo = new ProcessStartInfo(pathtobatfile);
                                    startinfo.Arguments = string.Format("{0} {1}", newdestFile4, Outputfile3);
                                    startinfo.UseShellExecute = false;
                                    process.StartInfo = startinfo;
                                    process.Start();
                                    process.WaitForExit();


                                    //transfer file back
                                    //Outputfile3 = Regex.Replace(Outputfile3, "[^A-Za-z0-9 ]", "");
                                    /*
                                    string Outputfile4 = Outputfile3.Remove(0, 11);
                                    string destFile4 = System.IO.Path.Combine(NewFolderLoc, Outputfile4);
                                    string compressedfilepath = System.IO.Path.Combine(NewFolderLoc, Outputfile3);*/
                                    string NewFolderLocfile = System.IO.Path.Combine(NewFolderLoc, Outputfile3);
                                    string Originalfile = System.IO.Path.Combine(NewFolderLoc, inputstringfull);
                                    System.IO.File.Move(NewFolderLocfile, Originalfile);
                                    System.IO.File.Delete(newdestFile4);
                                    System.IO.File.Delete(NewFolderLocfile);
                                    if (System.IO.Directory.Exists(savePath))
                                    {
                                        System.IO.File.Copy(Originalfile, sourceFile, true);
                                        System.IO.File.Delete(Originalfile);
                                    }
                                    /*
                                    string sourceFile1 = System.IO.Path.Combine(NewFolderLoc, Outputfile4);

                                        //var outfile3 = Outputfile3.Substring(Outputfile3.LastIndexOf("-") + 1);
                                        string destFile1 = System.IO.Path.Combine(savePath, Outputfile4);
                                    //check if dir path exisit
                                    if (System.IO.Directory.Exists(savePath))
                                        {
                                            System.IO.File.Copy(sourceFile1, destFile1,true);
                                            System.IO.File.Delete(sourceFile1);
                                        }
                                     */

                                }
                                else
                                {
                                    Console.WriteLine("Hi User, Please note that no valid PDF files have been found.");
                                }
                            }
                        }

                    }
                }
            }

        }

        private static void ChangeFilename(string oldcompressedfilepath, string newdestFile4)
        {
            try
            {
                System.IO.File.Move(oldcompressedfilepath, newdestFile4);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            return;
        }

    }
}

