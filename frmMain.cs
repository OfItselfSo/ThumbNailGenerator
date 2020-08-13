using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/// +------------------------------------------------------------------------------------------------------------------------------+
/// ¦                                                   TERMS OF USE: MIT License                                                  ¦
/// +------------------------------------------------------------------------------------------------------------------------------¦
/// ¦Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation    ¦
/// ¦files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,    ¦
/// ¦modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software¦
/// ¦is furnished to do so, subject to the following conditions:                                                                   ¦
/// ¦                                                                                                                              ¦
/// ¦The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.¦
/// ¦                                                                                                                              ¦
/// ¦THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE          ¦
/// ¦WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR         ¦
/// ¦COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,   ¦
/// ¦ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                         ¦
/// +------------------------------------------------------------------------------------------------------------------------------+


// This code is designed to semi-automate the production of scaling, cropping 600x600 images from portions of 
// files in a directory of images a 100x100 thumbnail will be produced at the same time. 

namespace ThumbNailGenerator
{

    /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
    /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
    /// <summary>
    /// A form to automate the production of thumbnail images - also includes
    /// the ability to scale the image
    /// <history>
    ///    01 Aug 20  Cynic - originally written
    /// </history>
    public partial class frmMain : Form
    {

        // this folder is the appended output
        private string DEFAULT_TOPLEVEL_IMAGE_DIRECTORY = @"Thumbnails";
        // we use this for sanity checks
        private int MINIMUM_ACCEPTABLE_THUMBNAIL_DIRECTORY_LENGTH = 6;

        private const string FILTER_JPG = "JPEG files (*.jpg)|*.jpg";
        private const string FILTER_EXT_JPG = "*.jpg";
        private const string BOXTITLE_JPG = "Choose JPEG File to Convert";

        private const string DEFAULT_CROPPED_IMAGE_SUFFIX = "_600x600";
        private const string DEFAULT_THUMBNAIL_IMAGE_SUFFIX = "_100x100";

        // this is the current scale factor of the image
        private const double MAX_SCALEFACTOR = 6;
        private const double MIN_SCALEFACTOR = 0.1;
        private const double DEFAULT_SCALEFACTOR = 1;
        private const double SCALEFACTOR_INCREMENT = 0.1;
        private double scaleFactor = DEFAULT_SCALEFACTOR;

        // this is the currently loaded full size image
        private Image fullSizeImage = null;

        // this is the scaling we use to display the image - we cannot show a 6000x4000 image
        private const int ONSCREEN_SCALING = 20;

        // the position of the upper left corner of the viewport
        private Point vpPosition = new Point(0, 0);

        // the nudge level of the view port
        private const int DEFAULT_VP_NUDGE_LEVEL = 5;
        private int vpNudgeLevel = DEFAULT_VP_NUDGE_LEVEL;

        // default sizes
        private const int STD_IMAGE_WIDTH = 6000;
        private const int STD_IMAGE_HEIGHT = 4000;

        private const int STD_OUT_WIDTH = 600;
        private const int STD_OUT_HEIGHT = 600;

        private const int STD_TH_WIDTH = 100;
        private const int STD_TH_HEIGHT = 100;

        // flags
        private bool inhibitScaleLevelChanges = false;

        private const char DEFAULT_AUTOINCREMENT_MARKER = 'a';
        private char autoIncrementMarker = DEFAULT_AUTOINCREMENT_MARKER;

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Main form entry point
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        public frmMain()
        {
            InitializeComponent();

            // set up our combo box with scroll levels
            inhibitScaleLevelChanges = true;
            for (double i = MIN_SCALEFACTOR; i <= MAX_SCALEFACTOR; i += 0.1)
            {
                comboBoxScaleLevel.Items.Add(i.ToString());
            }
            comboBoxScaleLevel.Text = DEFAULT_SCALEFACTOR.ToString();
            inhibitScaleLevelChanges = false;

            pictureBoxScaled.MouseWheel += new MouseEventHandler(this.pictureBoxScaled_MouseWheel);

            // recover the application settings
            RecoverAppSettings();

        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Form closing handler
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAppSettings();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Save the application settings
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void SaveAppSettings()
        {
            // save up our directorys
            Properties.Settings.Default.InputDirectory = InputDirectory;
            Properties.Settings.Default.OutputDirectory = OutputDirectory;
            Properties.Settings.Default.CroppedImageSuffix = CroppedImageSuffix;
            Properties.Settings.Default.ThumbnailImageSuffix = ThumbnailImageSuffix;
            Properties.Settings.Default.AutoLoadState = AutoLoadState;
            Properties.Settings.Default.Save();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Recover the application settings from the cache and set up the screen
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void RecoverAppSettings()
        {
            // set up our image directorys
            InputDirectory = Properties.Settings.Default.InputDirectory;
            OutputDirectory = Properties.Settings.Default.OutputDirectory;
            CroppedImageSuffix = Properties.Settings.Default.CroppedImageSuffix;
            ThumbnailImageSuffix = Properties.Settings.Default.ThumbnailImageSuffix;
            AutoLoadState = Properties.Settings.Default.AutoLoadState;

        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the image
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void buttonGetImage_Click(object sender, EventArgs e)
        {
            try
            {
                bool retBool;
                int retInt;
                string errMsg = "";

                // reset this
                AutoIncrementMarker = DEFAULT_AUTOINCREMENT_MARKER;

                // clean up our existing images
                if (fullSizeImage != null)
                {
                    Image tmpImage = fullSizeImage;
                    fullSizeImage = null;
                    tmpImage.Dispose();
                }
                if (pictureBoxScaled.Image != null)
                {
                    Image tmpImage = pictureBoxScaled.Image;
                    pictureBoxScaled.Image = null;
                    tmpImage.Dispose();
                }
                if (pictureBox600x600.Image != null)
                {
                    Image tmpImage = pictureBox600x600.Image;
                    pictureBox600x600.Image = null;
                    tmpImage.Dispose();
                }

                // validate the image directory
                retBool = TestDirectoryIsValidAndExistsAndOfferToCreateIt(InputDirectory, "image", false, false, ref errMsg);
                if (retBool == false)
                {
                    // we will have prompted - just leave
                    return;
                }

                string filePathAndName = "";
                retInt = PickFile(InputDirectory, ImageName, FILTER_JPG, BOXTITLE_JPG, ref filePathAndName);
                if (retInt < 0)
                {
                    return;
                }
                if (retInt > 0)
                {
                    MessageBox.Show("Error " + retInt.ToString() + " occurred when choosing the image File.\n\nPlease see the log file.");
                    return;
                }
                if ((filePathAndName == null) || (filePathAndName.Length == 0))
                {
                    MessageBox.Show("Error no file name returned when choosing the image File.\n\nPlease see the log file.");
                    return;
                }

                // open the image file
                OpenImageFile(filePathAndName);

            }
            catch (Exception ex)
            {
                string errMsg = "An error occurred\n\n" + ex.Message;
                MessageBox.Show(errMsg);
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Do everything to open an image file when given a path and name
        /// </summary>
        /// <param name="filePathAndName">the full file path and name</param>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void OpenImageFile(string filePathAndName)
        {

            if ((filePathAndName == null) || (filePathAndName.Length < 3))
            {
                MessageBox.Show("File Path and name is null or empty");
                return;
            }

            // open the file
            try
            {
                fullSizeImage = new Bitmap(filePathAndName);
                pictureBoxScaled.Image = ResizeImage(fullSizeImage, fullSizeImage.Size.Width / ONSCREEN_SCALING, fullSizeImage.Size.Height / ONSCREEN_SCALING);
                RedrawImageAtCurrentScale();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex.Message);
                return;
            }

            // record this
            ImageName = Path.GetFileName(filePathAndName);
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Picks a File off the disk
        /// </summary>
        /// <param name="filePathAndName">the full file path and name</param>
        /// <param name="initialDirectory">the initial directory</param>
        /// <param name="suggestedFile">The suggested file</param>
        /// <param name="fileFilter">the file filter we use for the open box</param>
        /// <param name="boxTitle">the title we use for the open box</param>
        /// <returns>z success, -ve cancel, +ve fail</returns>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private int PickFile(string initialDirectory, string suggestedFile, string fileFilter, string boxTitle, ref string filePathAndName)
        {
            filePathAndName = string.Empty;

            OpenFileDialog ofDialog = new OpenFileDialog();

            if ((fileFilter == null) || (fileFilter.Length == 0))
            {
                return -100;
            }
            if ((boxTitle == null) || (boxTitle.Length == 0))
            {
                return -101;
            }

            ofDialog.Filter = fileFilter;

            // can we set the initial directory? Perform some checks
            // if we can't set it we just go with whatever windows has
            // as a default
            if (initialDirectory != null)
            {
                if (Directory.Exists(initialDirectory) == true)
                {
                    // set it
                    ofDialog.InitialDirectory = initialDirectory;
                }
            }
            // can we set the suggested File? 
            if (suggestedFile != null)
            {
                // set it
                ofDialog.FileName = suggestedFile;
            }
            ofDialog.Title = boxTitle;
            // Show it
            DialogResult dlgRes = ofDialog.ShowDialog();
            if (dlgRes != DialogResult.OK)
            {
                return -1;
            }
            // get the file name now
            filePathAndName = ofDialog.FileName;
            if ((filePathAndName == null) || (filePathAndName.Length == 0))
            {
                return 98;
            }
            // return it
            return 0;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Does everything necessary to redraw the current image at scale and according
        /// to the currently set view port
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void RedrawImageAtCurrentScale()
        {
            // clean up
            if (pictureBox600x600.Image != null)
            {
                Image tmpImage = pictureBox600x600.Image;
                pictureBox600x600.Image = null;
                tmpImage.Dispose();
            }

            // sanity check
            if (fullSizeImage == null) return;

            using (Bitmap bmp = new Bitmap(fullSizeImage))
            {
                // these are the proposed versions of the rectangle in the viewport
                int proposedXStart = (ONSCREEN_SCALING * vpPosition.X);
                int proposedYStart = (ONSCREEN_SCALING * vpPosition.Y);
                int proposedWidth = ONSCREEN_SCALING * VPXScaledSize;
                int proposedHeight = ONSCREEN_SCALING * VPYScaledSize;

                // sanity check
                if (proposedXStart < 0) proposedXStart = 0;
                if (proposedYStart < 0) proposedYStart = 0;

                // the end point must be on the screen
                if (proposedXStart + proposedWidth >= bmp.Width)
                {
                    // move it back
                    proposedXStart = bmp.Width - (ONSCREEN_SCALING * VPXScaledSize);
                }
                if (proposedYStart + proposedHeight >= bmp.Height)
                {
                    // move it back
                    proposedYStart = bmp.Height - (ONSCREEN_SCALING * VPYScaledSize);
                }

                // last sanity check
                if ((proposedXStart < 0) || (proposedYStart < 0))
                {
                    proposedXStart = 0;
                    proposedYStart = 0;
                    proposedWidth = fullSizeImage.Size.Width;
                    proposedHeight = fullSizeImage.Size.Height;
                }

                // make a new image of the rectangle enclosed by the viewport
                Bitmap newImg = bmp.Clone(new Rectangle { X = proposedXStart, Y = proposedYStart, Width = proposedWidth, Height = proposedHeight }, bmp.PixelFormat);
                pictureBox600x600.Image = ResizeImage(newImg, STD_OUT_WIDTH, STD_OUT_HEIGHT);
                if(newImg!=null)
                {
                    newImg.Dispose();
                }
            }

            // repaint the scaled picture box
            pictureBoxScaled.Invalidate();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Resize the image to the specified width and height. 
        /// 
        /// Source:
        /// https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
        /// 
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        /// <history>
        ///    01 Aug 20  Cynic - Obtained from Stackoverflow
        /// </history>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a press on the Set Input Directory button
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private void buttonSetInputDirectory_Click(object sender, EventArgs e)
        {
            bool retBool;
            string errMsg = "";

            try
            {
                // reset this
                AutoIncrementMarker = DEFAULT_AUTOINCREMENT_MARKER;

                // get the output directory 
                string outDir;
                // set the initial directory to the last value and call the function to
                // set the directory
                retBool = GetDirectoryByPrompt(InputDirectory, "Set Input Directory", true, out outDir);
                if (retBool == false) return;
                if ((outDir == null) || (outDir.Length == 0)) return;

                // validate the image directory
                retBool = TestDirectoryIsValidAndExistsAndOfferToCreateIt(outDir, "Images", false, false, ref errMsg);
                if (retBool == false)
                {
                    // we will have prompted - justleave
                    return;
                }
                // set it now
                InputDirectory = outDir;
                //this.Cursor = System.Windows.Forms.Cursors.WaitCursor; 
            }
            finally
            {
                //this.Cursor = System.Windows.Forms.Cursors.Default; 
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the autoload state. 
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool AutoLoadState
        {
            get
            {
                return checkBoxAutoLoadNext.Checked;
            }
            set
            {
                checkBoxAutoLoadNext.Checked = value;
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the cropped image suffix. Will return default on null or empty
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string CroppedImageSuffix
        {
            get
            {
                if ((textBoxCroppedImageSuffix.Text == null) || (textBoxCroppedImageSuffix.Text.Length < 3)) textBoxCroppedImageSuffix.Text = DEFAULT_CROPPED_IMAGE_SUFFIX;
                return textBoxCroppedImageSuffix.Text;
            }
            set
            {
                textBoxCroppedImageSuffix.Text = value;
                if ((textBoxCroppedImageSuffix.Text == null) || (textBoxCroppedImageSuffix.Text.Length < 3)) textBoxCroppedImageSuffix.Text = DEFAULT_CROPPED_IMAGE_SUFFIX;
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the thumbnail image suffix. Will return default on null or empty
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string ThumbnailImageSuffix
        {
            get
            {
                if ((textBoxThumbnailImageSuffix.Text == null) || (textBoxThumbnailImageSuffix.Text.Length < 3)) textBoxThumbnailImageSuffix.Text = DEFAULT_THUMBNAIL_IMAGE_SUFFIX;
                return textBoxThumbnailImageSuffix.Text;
            }
            set
            {
                textBoxThumbnailImageSuffix.Text = value;
                if ((textBoxThumbnailImageSuffix.Text == null) || (textBoxThumbnailImageSuffix.Text.Length < 3)) textBoxThumbnailImageSuffix.Text = DEFAULT_THUMBNAIL_IMAGE_SUFFIX;
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the image name. Will never return null will return
        /// empty if anything is dodgy at all
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string ImageName
        {
            get
            {
                if ((textBoxImageName.Text == null) || (textBoxImageName.Text.Length < 3)) textBoxImageName.Text = "";
                return textBoxImageName.Text;
            }
            set
            {
                textBoxImageName.Text = value;
                if ((textBoxImageName.Text == null) || (textBoxImageName.Text.Length < 3)) textBoxImageName.Text = "";
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the input directory. Will never return null will return
        /// default input file if anything is dodgy at all
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string InputDirectory
        {
            get
            {
                if ((textBoxInputDirectory.Text == null) || (textBoxInputDirectory.Text.Length < 3)) textBoxInputDirectory.Text = GetDefaultInputDirectory();
                return textBoxInputDirectory.Text;
            }
            set
            {
                textBoxInputDirectory.Text = value;
                if ((textBoxInputDirectory.Text == null) || (textBoxInputDirectory.Text.Length < 3)) textBoxInputDirectory.Text = GetDefaultInputDirectory();
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the output directory. Will never return null will return
        /// default output file if anything is dodgy at all
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string OutputDirectory
        {
            get
            {
                if ((textBoxOutputDirectory.Text == null) || (textBoxOutputDirectory.Text.Length < 3)) textBoxOutputDirectory.Text = GetDefaultOutputDirectory();
                return textBoxOutputDirectory.Text;
            }
            set
            {
                textBoxOutputDirectory.Text = value;
                if ((textBoxOutputDirectory.Text == null) || (textBoxOutputDirectory.Text.Length < 3)) textBoxOutputDirectory.Text = GetDefaultOutputDirectory();
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Encapsulates everything required to get a folder by user interaction.
        /// </summary>
        /// <param name="boxTitle">Box Title</param>
        /// <param name="outDir">The output directory is returned in here</param>
        /// <param name="showNewFolder">if true show new folder button</param>
        /// <param name="startDir">the starting directory</param>
        /// <returns>true the user pressed ok, false the user cancelled</returns>
        /// <history>
        ///     01 Aug 20  Cynic - originally written
        /// </history>
        public static bool GetDirectoryByPrompt(string startDir, string boxTitle, bool showNewFolder, out string outDir)
        {
            outDir = "";
            FolderBrowserDialog frmFolderBrowser = new FolderBrowserDialog();
            frmFolderBrowser.ShowNewFolderButton = true;
            frmFolderBrowser.SelectedPath = startDir;
            DialogResult dlgRes = frmFolderBrowser.ShowDialog();
            if (dlgRes != DialogResult.OK) return false;
            outDir = frmFolderBrowser.SelectedPath;
            return true;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the default input directory
        /// </summary>
        /// <returns>default input directory</returns>
        /// <history>
        ///    22 May 11  Cynic - Started
        /// </history>
        public string GetDefaultInputDirectory()
        {
            string appPath;
            // we use mydocuments as the start
            appPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            appPath = Path.Combine(appPath, DEFAULT_TOPLEVEL_IMAGE_DIRECTORY);
            return appPath;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the default output directory
        /// </summary>
        /// <returns>default output directory</returns>
        /// <history>
        ///    22 May 11  Cynic - Started
        /// </history>
        public string GetDefaultOutputDirectory()
        {
            string appPath;
            // we use mydocuments as the start
            appPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            appPath = Path.Combine(appPath, DEFAULT_TOPLEVEL_IMAGE_DIRECTORY);
            return appPath;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Validity checks the output directory
        /// </summary>
        /// <returns>true, directory exists and is valid, false it is not</returns>
        /// <param name="beSilent">if true we do not prompt, just ruturns</param>
        /// <param name="offerToCreate">if true we offer to create</param>
        /// <param name="dirNameText">a name for the directory for the prompts</param>
        /// <param name="errMsg">we return the error message in here</param>
        /// <param name="directoryPath">directory path to validate</param>
        /// <history>
        ///     01 Aug 20  Cynic - originally written
        /// </history>
        public bool TestDirectoryIsValidAndExistsAndOfferToCreateIt(string directoryPath, string dirNameText, bool beSilent, bool offerToCreate, ref string errMsg)
        {
            // set this now
            errMsg = "Unknown Error";
            string testableDirectory = "";

            if (directoryPath == null)
            {
                errMsg = "The " + dirNameText + " directory is not valid. Null value specified";
                return false;
            }
            if (dirNameText == null) dirNameText = "";

            // we want to remove any relative paths that may have snuck in here
            try
            {
                testableDirectory = Path.GetFullPath(directoryPath);
                if (testableDirectory == null)
                {
                    // should never happen
                    errMsg = "The " + dirNameText + " directory " + directoryPath + " is not valid. FullPath is null";
                    if (beSilent == false) MessageBox.Show(errMsg);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = "Error resolving relative paths in " + dirNameText + " directory " + InputDirectory + "\n\n" + ex.Message;
                MessageBox.Show(errMsg);
                return false;
            }

            // sanity check the directory length
            if (testableDirectory.Length < MINIMUM_ACCEPTABLE_THUMBNAIL_DIRECTORY_LENGTH)
            {
                // should never happen
                errMsg = "The " + dirNameText + " directory " + testableDirectory + " is not valid. Length = " + MINIMUM_ACCEPTABLE_THUMBNAIL_DIRECTORY_LENGTH.ToString();
                if (beSilent == false) MessageBox.Show(errMsg);
                return false;
            }

            // test the directory for validity
            if (Path.IsPathRooted(testableDirectory) == false)
            {
                // not good, not having this
                errMsg = "The " + dirNameText + " directory " + testableDirectory + " is not valid. Not rooted";
                if (beSilent == false) MessageBox.Show(errMsg);
                return false;
            }

            // test the directory exists
            if (Directory.Exists(testableDirectory) == false)
            {
                errMsg = "The " + dirNameText + " directory " + testableDirectory + " does not exist.";
                if (beSilent == true) return false;

                // prompt the user
                DialogResult outVal = MessageBox.Show("Directory does not exist", "The " + dirNameText + " directory " + testableDirectory + " does not exist.\n\nDo you want to create it now.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (outVal == System.Windows.Forms.DialogResult.Yes)
                {
                    // we create the directory
                    try
                    {
                        // create the directory now
                        Directory.CreateDirectory(testableDirectory);
                    }
                    catch (Exception ex)
                    {
                        errMsg = "Error creating " + dirNameText + " directory " + testableDirectory + "\n\n" + ex.Message;
                        MessageBox.Show(errMsg);
                        return false;
                    }
                    // retest
                    if (Directory.Exists(testableDirectory) == false)
                    {
                        errMsg = "Retest, " + dirNameText + " Directory still does not exist\n\n" + testableDirectory;
                        MessageBox.Show(errMsg);
                    }
                }
            }
            return true;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the next filename from the existing one in the directory. Will 
        /// filter out names with the cropped and thumbnail image suffixes
        /// </summary>
        /// <param name="path">the directory to look in</param>
        /// <param name="startingFileName">the starting filename</param>
        /// <param name="filter">the extension filter</param>
        /// <param name="wantGoBack">if true get the previous image not the next</param>
        /// <returns>the full path and name of the new filename or null for none left</returns>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private string GetNextFileName(string path, string startingFileName, string filter, bool wantGoBack)
        {
            bool foundStartingFile = false;
            string lastFileName = "";

            // get all of the files in the directory
            string[] files = Directory.GetFiles(path, filter);

            for (int i = 0; i < files.Length; i++)
            {
                // get the filename from the directory list (it contains the full path)
                string tmpFileName = Path.GetFileName(files[i]);

                // have we found the starting file
                if (foundStartingFile == false)
                {
                    // is it the starting filename
                    if (tmpFileName == startingFileName)
                    {
                        foundStartingFile = true;
                        continue;
                    }
                    else
                    {
                        // record the last file name which is not excluded
                        if (tmpFileName.Contains(CroppedImageSuffix) == true) continue;
                        if (tmpFileName.Contains(ThumbnailImageSuffix) == true) continue;
                        // this is it
                        lastFileName = files[i];
                        continue;
                    }
                }
                else
                {
                    // do we want to go back?
                    if (wantGoBack == true)
                    {
                        // return this if it is empty that is ok we are just at the start
                        return lastFileName;
                    }
                    else
                    {
                        // we have found the starting file, the next valid one is the one we want
                        if (tmpFileName.Contains(CroppedImageSuffix) == true) continue;
                        if (tmpFileName.Contains(ThumbnailImageSuffix) == true) continue;
                        // this is the one. We return the full path and name
                        return files[i];
                    }
                }
            }
            // not found
            return "";
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Returns the VP Position given the center position
        /// </summary>
        /// <param name="pointCenterPos">the center positon</param>
        /// <returns>the upper left hand corner from the center position</returns>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private Point VPXPositionFromCenter(Point pointCenterPos)
        {
            int tmpXPos = pointCenterPos.X - (VPXScaledSize / 2);
            int tmpYPos = pointCenterPos.Y - (VPYScaledSize / 2);

            // sanity checks - cannot be less than zero
            if (tmpXPos <= 0) tmpXPos = 0;
            if (tmpYPos <= 0) tmpYPos = 0;

            return new Point(tmpXPos, tmpYPos);
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Sanity checks a view port position
        /// </summary>
        /// <param name="vpPositionIn">the view port position</param>
        /// <returns>updates the incoming vpPosition</returns>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private Point SanityCheckVPPosition(Point vpPositionIn)
        {
            // check off screen to top and left
            if (vpPositionIn.X < 0) vpPositionIn.X = 0;
            if (vpPositionIn.Y < 0) vpPositionIn.Y = 0;

            if (pictureBoxScaled.Image != null)
            {
                // check offscreen to bottom and right
                if ((vpPositionIn.X + VPXScaledSize) >= pictureBoxScaled.Image.Size.Width) vpPositionIn.X = pictureBoxScaled.Image.Size.Width - VPXScaledSize - 1;
                if ((vpPositionIn.Y + VPYScaledSize) >= pictureBoxScaled.Image.Size.Height) vpPositionIn.Y = pictureBoxScaled.Image.Size.Height - VPYScaledSize - 1;
            }

            return vpPositionIn;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the X scaled size
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private int VPXScaledSize
        {
            get
            {
                // calc from the global scale factor
                return CalcScaledSizeFromScaleFactor(scaleFactor);
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the Y scaled size
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private int VPYScaledSize
        {
            get
            {
                // calc from the global scale factor
                return CalcScaledSizeFromScaleFactor(scaleFactor);
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Calcs the scaled size from a specified scale factor
        /// </summary>
        /// <param name="scaleFactorIn">the scale factor</param>
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private int CalcScaledSizeFromScaleFactor(double scaleFactorIn)
        {
            return (int)((STD_OUT_HEIGHT * scaleFactorIn) / ONSCREEN_SCALING);
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles the paint event on the full size image picture box
        /// 
        /// Source:
        /// https://stackoverflow.com/questions/5941979/c-sharp-drawing-a-rectangle-on-a-picturebox
        /// 
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - Obtained from Stackoverflow
        /// </history>
        private void pictureBoxScaled_Paint(object sender, PaintEventArgs e)
        {
            // always do this 
            vpPosition = SanityCheckVPPosition(vpPosition);
            // we draw the viewport on the screen
            Rectangle ee = new Rectangle(vpPosition.X, vpPosition.Y, VPYScaledSize, VPXScaledSize);
            using (Pen pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, ee);
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a click on the nudge button
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void buttonVPUp_Click(object sender, EventArgs e)
        {
            vpPosition.Y = vpPosition.Y - vpNudgeLevel;
            vpPosition = SanityCheckVPPosition(vpPosition);
            // we need a refresh
            RedrawImageAtCurrentScale();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a click on the nudge button
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void buttonVPDown_Click(object sender, EventArgs e)
        {
            vpPosition.Y = vpPosition.Y + vpNudgeLevel;
            vpPosition = SanityCheckVPPosition(vpPosition);
            // we need a refresh
            RedrawImageAtCurrentScale();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a click on the nudge button
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void buttonVPRight_Click(object sender, EventArgs e)
        {
            vpPosition.X = vpPosition.X + vpNudgeLevel;
            vpPosition = SanityCheckVPPosition(vpPosition);
            // we need a refresh
            RedrawImageAtCurrentScale();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a click on the nudge button
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void buttonVPLeft_Click(object sender, EventArgs e)
        {
            vpPosition.X = vpPosition.X - vpNudgeLevel;
            vpPosition = SanityCheckVPPosition(vpPosition);
            // we need a refresh
            RedrawImageAtCurrentScale();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a mouse click on the scaled picture box
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - Obtained from Stackoverflow
        /// </history>
        private void pictureBoxScaled_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null)
            {
                vpPosition = VPXPositionFromCenter(mouseEventArgs.Location);
                vpPosition = SanityCheckVPPosition(vpPosition);
                // we need a refresh
                RedrawImageAtCurrentScale();
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a change on the scroll factor combo box
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void comboBoxScaleLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inhibitScaleLevelChanges == true) return;

            // get the existing scale factor
            double oldScaleFactor = scaleFactor;
            // get the current setting
            double newScaleFactor = Convert.ToDouble(comboBoxScaleLevel.Text);
            double scaleDiff = newScaleFactor - oldScaleFactor;
            // offset the viewPort position so the center stays centered after the change in scale
            vpPosition.X = vpPosition.X - (int)(STD_OUT_HEIGHT * scaleDiff) / (2 * ONSCREEN_SCALING);
            vpPosition.Y = vpPosition.Y - (int)(STD_OUT_HEIGHT * scaleDiff) / (2 * ONSCREEN_SCALING);
            // set this now
            scaleFactor = newScaleFactor;
            // we need a refresh
            RedrawImageAtCurrentScale();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a click on the nudge level radio button
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void radioButtonVPNudge1_CheckedChanged(object sender, EventArgs e)
        {
            vpNudgeLevel = 1;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a click on the nudge level radio button
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void radioButtonVPNudge3_CheckedChanged(object sender, EventArgs e)
        {
            vpNudgeLevel = 3;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a click on the nudge level radio button
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void radioButtonVPNudge5_CheckedChanged(object sender, EventArgs e)
        {
            vpNudgeLevel = 5;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a click on the nudge level radio button
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void radioButtonVPNudge10_CheckedChanged(object sender, EventArgs e)
        {
            vpNudgeLevel = 10;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a rotations of the mousewheel when the scaled picture box is in focus
        /// 
        /// <history>
        ///    01 Aug 20  Cynic - originally written
        /// </history>
        private void pictureBoxScaled_MouseWheel(object sender, MouseEventArgs e)
        {
            // get the current scale factor setting from the combo box
            double currentScaleFactor = Convert.ToDouble(comboBoxScaleLevel.Text);

            // adjust it up or down according to the mouse wheel move
            if (e.Delta > 0) currentScaleFactor += SCALEFACTOR_INCREMENT;
            else currentScaleFactor -= SCALEFACTOR_INCREMENT;

            // sanity checks
            if (currentScaleFactor < MIN_SCALEFACTOR) currentScaleFactor = MIN_SCALEFACTOR;
            if (currentScaleFactor > MAX_SCALEFACTOR) currentScaleFactor = MAX_SCALEFACTOR;

            // this will trigger the re-draw the same as if the user set it themselves
            comboBoxScaleLevel.Text = currentScaleFactor.ToString();
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a press on the Set Output Directory button
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private void buttonSetOutputDirectory_Click(object sender, EventArgs e)
        {
            bool retBool;
            string errMsg = "";

            try
            {

                // reset this
                AutoIncrementMarker = DEFAULT_AUTOINCREMENT_MARKER;

                // get the output directory 
                string outDir;
                // set the initial directory to the last value and call the function to
                // set the directory
                retBool = GetDirectoryByPrompt(InputDirectory, "Set Output Directory", true, out outDir);
                if (retBool == false) return;
                if ((outDir == null) || (outDir.Length == 0)) return;

                // validate the image directory
                retBool = TestDirectoryIsValidAndExistsAndOfferToCreateIt(outDir, "Images", false, false, ref errMsg);
                if (retBool == false)
                {
                    // we will have prompted - justleave
                    return;
                }
                // set it now
                OutputDirectory = outDir;
                //this.Cursor = System.Windows.Forms.Cursors.WaitCursor; 
            }
            finally
            {
                //this.Cursor = System.Windows.Forms.Cursors.Default; 
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a press on the save image button
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private void buttonSaveImage_Click(object sender, EventArgs e)
        {
            // reset this
            AutoIncrementMarker = DEFAULT_AUTOINCREMENT_MARKER;

            int retInt = SaveCurrentImage("");
            if(retInt!=0)
            {
                // user has already been told
                return;
            }

            // do we want to automatically load the next image
            if (AutoLoadState == true)
            {
                // this does it all
                LoadNextImage(false);
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a press on the save+duplicate image button
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private void buttonSaveAndDuplicateImage_Click(object sender, EventArgs e)
        {

            int retInt = SaveCurrentImage(AutoIncrementMarker.ToString());
            if(retInt!=0)
            {
                // user has already been told
                return;
            }

            // increment this. We never anticpate needing to go all the way to 'z'
            AutoIncrementMarker++;

        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Saves the current image
        /// </summary>
        /// <param name="incrementMarker">a string we append to the base file name. Can be empty </param>
        /// <returns>z success, nz fail</returns>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private int SaveCurrentImage(string incrementMarker)
        { 
            try
            {
                string outFileNameAndPath = null;

                if (incrementMarker == null) incrementMarker = "";

                // is there anything loaded
                if (pictureBox600x600.Image == null)
                {
                    MessageBox.Show("There is no image loaded");
                    return -1;
                }
                // sanity check this
                if ((ImageName == null) || (ImageName.Length == 0))
                {
                    MessageBox.Show("There is no image name");
                    return -2;
                }

                // test the output directory exists
                if (Directory.Exists(OutputDirectory) == false)
                {
                    string errMsg = "Directory does not exist\n\n" + OutputDirectory;
                    MessageBox.Show(errMsg);
                    return -3;
                }

                // get the filename part
                string baseFileName = Path.GetFileNameWithoutExtension(ImageName);
                if ((baseFileName == null) || (baseFileName.Length == 0))
                {
                    MessageBox.Show("There is no baseFileName");
                    return -4;
                }
                // compose the cropped file output path
                outFileNameAndPath = Path.Combine(OutputDirectory, baseFileName + incrementMarker + CroppedImageSuffix + ".jpg");
                // save the cropped image file
                pictureBox600x600.Image.Save(outFileNameAndPath, ImageFormat.Jpeg);

                // compose the thumbnail file output path
                outFileNameAndPath = Path.Combine(OutputDirectory, baseFileName + incrementMarker + ThumbnailImageSuffix + ".jpg");
                // shrink the cropped file down to a thumbnail
                Image Image100x100 = ResizeImage(pictureBox600x600.Image, STD_TH_WIDTH, STD_TH_HEIGHT);
                // save the cropped image file
                Image100x100.Save(outFileNameAndPath, ImageFormat.Jpeg);


            }
            catch (Exception ex)
            {
                string errMsg = "An error occurred\n\n" + ex.Message;
                MessageBox.Show(errMsg);
                return -100;
            }
            return 0;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a press on the skip image button
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private void buttonSkipImage_Click(object sender, EventArgs e)
        {
            try
            {
                // reset this
                AutoIncrementMarker = DEFAULT_AUTOINCREMENT_MARKER;

                // this does it all
                LoadNextImage(false);
            }
            catch (Exception ex)
            {
                string errMsg = "An error occurred\n\n" + ex.Message;
                MessageBox.Show(errMsg);
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Loads the next image
        /// </summary>
        /// <param name="wantGoBack">if true get the previous image not the next</param>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private void LoadNextImage(bool wantGoBack)
        {
            // sanity check this
            if ((ImageName == null) || (ImageName.Length == 0))
            {
                MessageBox.Show("There is no current name");
                return;
            }
            // test the input directory exists
            if (Directory.Exists(InputDirectory) == false)
            {
                string errMsg = "Directory does not exist\n\n" + InputDirectory;
                MessageBox.Show(errMsg);
                return;
            }
            // get the file name
            string nextFilePathAndName = GetNextFileName(InputDirectory, ImageName, FILTER_EXT_JPG, wantGoBack);
            if ((nextFilePathAndName == null) || (nextFilePathAndName.Length <= 3))
            {
                MessageBox.Show("All files processed");
                return;
            }

            // open the image file
            OpenImageFile(nextFilePathAndName);

        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Handles a press on the go back button
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private void buttonGoBack_Click(object sender, EventArgs e)
        {
            try
            {
                // reset this
                AutoIncrementMarker = DEFAULT_AUTOINCREMENT_MARKER;

                // this does it all
                LoadNextImage(true);
            }
            catch (Exception ex)
            {
                string errMsg = "An error occurred\n\n" + ex.Message;
                MessageBox.Show(errMsg);
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets/Sets the autoincrement marker
        /// </summary>
        /// <history>
        ///    01 Aug 20  Cynic - Orginally Written
        /// </history>
        private char AutoIncrementMarker
        {
            get
            {
                return autoIncrementMarker;
            }
            set
            {
                autoIncrementMarker = value;
            }
        }

    }
}
