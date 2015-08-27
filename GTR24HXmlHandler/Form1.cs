using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;
using System.Xml.Linq;

namespace GTR24HXmlHandler
{
    public partial class Form1 : Form
    {

        private FileSystemWatcher fileWatcher;
        private bool isWatching;
        private readonly string toPath= @"C:\CopyTo\";


        public Form1()
        {
            InitializeComponent();
            btnStartWatch.Enabled = false;
            btnHandleExisting.Enabled = false;
            fileWatcher = new FileSystemWatcher();
            fileWatcher.SynchronizingObject = this;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult resDialog = folderBrowserDialog.ShowDialog();
            if (resDialog.ToString() == "OK")
            {
                tbFileLocation.Text = folderBrowserDialog.SelectedPath;
                if (tbFileLocation.Text != "")
                {
                    btnStartWatch.Enabled = true;
                    btnHandleExisting.Enabled = true;
                }
            }
        }

        private void btnStartWatch_Click(object sender, EventArgs e)
        {
           
            var fileCount = Directory.EnumerateFiles(tbFileLocation.Text, "*.xml", SearchOption.TopDirectoryOnly).Count();
            if (fileCount == 0) {
                         

                if (isWatching)
                {
                    isWatching = false;
                    fileWatcher.EnableRaisingEvents = false;
                    lblInfo.Text = "Stopped";
                    //fileWatcher.Dispose();
                    btnStartWatch.BackColor = Color.LightSkyBlue;
                    btnStartWatch.Text = "Start Watching";
                }
                else
                {
                    isWatching = true;

                    lblInfo.Text = "Running";

                     fileWatcher.Filter = "*.xml*";
                    fileWatcher.Path = tbFileLocation.Text + "\\";

                    fileWatcher.NotifyFilter= NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;

                    fileWatcher.Created += new FileSystemEventHandler(OnChanged);
                    fileWatcher.EnableRaisingEvents = true;
                    btnStartWatch.BackColor = Color.LightSalmon;
                    btnStartWatch.Text = "Stop Watching";
                }
            }
            else
            {
                lblInfo.Text = "Watching cannot be started since there are unprocessed files in the directory.";
            }
           
        }

        private void btnHandleExisting_Click(object sender, EventArgs e)
        {
            if (isWatching)
            {
                lblInfo.Text = "The folder is already being watched";
            }
            else
            {
                var files = Directory.EnumerateFiles(tbFileLocation.Text + "\\","*.xml");

                if (files.Any())
                {
                    var count = 0;
                    foreach (var file in files)
                    {
                        count++;
                        var fromPath = file;
                        var addData = ExtractDataFromFile(fromPath);
                        var fileName = file.Substring(file.LastIndexOf("\\") + 1);
                        if (addData)
                        {
                           
                            File.Move(fromPath, toPath + fileName);
                           
                            //Thread.Sleep(2000);
                            //lblInfo.Text = "Running";


                        }
                    }
                    lblInfo.Text = "Processed no." + count;
                }
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            var fromPath = e.FullPath;
            var addData = ExtractDataFromFile(fromPath);
            if (addData)
            {
                                
                File.Move(fromPath, toPath + e.Name);
                lblInfo.Text = "it worked";
                Thread.Sleep(3000);
                lblInfo.Text = "Running";


            }
            
        }

        private bool ExtractDataFromFile(string filePath)
        {

            XDocument xmlDoc = null;

            //try
            //{
            //    xmlDoc.Load(filePath);
            //    if (xmlDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
            //    {
            //        XmlDeclaration dec = (XmlDeclaration)xmlDoc.FirstChild;
            //        dec.Encoding = "windows-1250";
            //    }
            //    else
            //    {
            //        XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", null, null);
            //        dec.Encoding = "windows-1250";
            //        xmlDoc.InsertBefore(dec, xmlDoc.DocumentElement);
            //    }
            //}
            //catch (Exception e)
            //{

            //    Logging.AddLogEntry(filePath + " : "+ e.Message);
            //    return true;
            //}
            using (StreamReader docReader = new StreamReader(filePath, Encoding.GetEncoding("windows-1250")))
            {
                try
                {
                    xmlDoc = XDocument.Load(docReader);
                }
                catch (Exception)
                {

                    Logging.AddLogEntry(filePath + " : Root element is corrupted");
                    return true;
                }


                var nodePossibilities = new List<string> {"Practice1","Warmup","Qualify","Race"};//rFactorXML/RaceResults/Warmup/Driver
                var xelRoot = xmlDoc.Root;
                var nodeExists = "";

                foreach (var item in nodePossibilities)
                {
                    var test = xmlDoc.Root.Element("RaceResults").Element(item);
                    if (test != null)
                    {
                        nodeExists = item;
                        break;
                    }
                }
                if (nodeExists == "")
                {
                    return true;
                }
                foreach (var driver in xmlDoc.Root.Element("RaceResults").Element(nodeExists).Elements("Driver"))
                {
                    if (driver != null)
                    {


                        var driverName = "";
                        var carModel = "";
                        var carClass = "";
                        var laps = 0;
                        var lapsUnder = 0;
                        foreach (XElement childNode in driver.Elements())
                        {
                            if(childNode.Name == "isPlayer"&& childNode.Value == "0")
                            {
                                goto BreakOut;
                            }
                            if (childNode.Name == "Name")
                            {
                                driverName = childNode.Value;
                            }
                            if (childNode.Name == "CarType")
                            {
                                carModel = childNode.Value;
                            }
                            if (childNode.Name == "CarClass")
                            {
                                carClass = childNode.Value;
                            }
                            if (childNode.Name == "Lap")
                            {
                                //First we have to establise if it's a full lap
                                var isFullLap = false;
                                var attributes = childNode.Attributes();
                                foreach (XAttribute attribute in attributes)
                                {
                                    //If it's a full lap then s1,s2 and s3 attributes are present in the lap-node
                                    if (attribute.Name == "s1" || attribute.Name == "s2" || attribute.Name == "s3")
                                    {
                                        isFullLap = IsAttributePresentOnNode(childNode, attribute.Name.ToString());
                                    }
                                }
                                var lapTimeText = childNode.Value.ToString();
                                var dotIndex = lapTimeText.IndexOf(".");
                                var s = lapTimeText.Substring(0, dotIndex);
                                Int16 num;
                                var isNumber = Int16.TryParse(s, out num);

                                //If the lap is a full lap and the car is a GTE class, we add a lap to the lapcount
                                if (isFullLap && isNumber && carClass == "GTE")
                                {
                                    laps++;
                                }
                                //If the car is a LMP, then we have to confirm that the laptime is faster than 3:30 (210 seconds)
                                if (isFullLap && isNumber && carClass == "LMP")
                                {
                                    laps++;

                                    //var lapTime = Convert.ToInt16(lapTimeText.Substring(0, dotIndex + 1));
                                    if (isNumber && num < 210)
                                    {
                                        lapsUnder++;
                                    }
                                }



                            }

                        }

                        //Lets see if the driver has done any previous laps in that type of car
                        var qualificationObject = DataBaseService.GetQualificationByNameAndModel(driverName, carModel);
                        //If the driver is already qualified, we stop here
                        if (qualificationObject.Qualified == true)
                        {
                            break;
                        }


                        qualificationObject.DriverName = driverName;
                        qualificationObject.CarModel = carModel;
                        qualificationObject.Class = carClass;
                        var test0 = qualificationObject.CompletedLaps;
                        qualificationObject.CompletedLaps += laps;
                        var test1 = qualificationObject.CompletedLaps;
                        if (carClass == "GTE" && qualificationObject.CompletedLaps > 49)
                        {
                            qualificationObject.Qualified = true;
                        }
                        if (carClass == "LMP" && !qualificationObject.TenInARow && lapsUnder > 9)
                        {
                            qualificationObject.TenInARow = true;

                        }
                        if (carClass == "LMP" && qualificationObject.CompletedLaps > 49 && qualificationObject.TenInARow)
                        {
                            qualificationObject.Qualified = true;
                        }

                        DataBaseService.AddQualificationInstance(qualificationObject);



                    BreakOut:
                        continue;
                    }
                
                }
                
                return true;
            }
        }

        private static bool IsAttributePresentOnNode(XElement node, string attrName)
        {
            var isPresent = false;
            var attr = node.Attributes(attrName);
            if (attr != null)
            {
                isPresent = true;
            }

            return isPresent;
        }
    }
}
