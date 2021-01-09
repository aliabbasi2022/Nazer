using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Video.DirectShow;
using Accord.Video.FFMPEG;
using System.Management;
using Accord.Video;
using System.Threading;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;

namespace Child
{
    public class Webcam
    {
        protected VideoCaptureDevice videoSource;
        protected object ImageBox;
        protected Bitmap Picture;
        public bool Runing;
        protected bool LastStatus;
        Semaphore PictureSM;
        Action<object> CallBackFunc;
        FilterInfoCollection videosources;
        public static Thread CallBackFunction;
        /// <summary>
        /// Initial and Detect WebCam with Max Resolutain 
        /// </summary>
        /// <param name="Target">Picture Box that Show WebCam Video</param>
        public Webcam(ref PictureBox Target)
        {
            Runing = false;
            LastStatus = false;
            ImageBox = Target;
            PictureSM = new Semaphore(1, 1);
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Check if atleast one video source is available
            if (videosources != null)
            {
                //For example use first video device. You may check if this is your webcam.
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);

                try
                {
                    //Check if the video device provides a list of supported resolutions
                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";
                        //Search for the highest resolution
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            if (videoSource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                                highestSolution = videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                        }
                        //Set the highest resolution as active
                        videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                    }
                }
                catch { }

                //Create NewFrame event handler
                //(This one triggers every time a new frame/image is captured
                videoSource.NewFrame += new Accord.Video.NewFrameEventHandler(videoSource_NewFrame);

                
            }
        }
        /// <summary>
        /// Initial and Detect WebCam with Max Resolutain 
        /// </summary>
        public Webcam(Accord.Video.NewFrameEventHandler Handler)
        {
            //ImageBox = Target;
            Runing = false;
            LastStatus = false;
            PictureSM = new Semaphore(1, 1);
            videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Check if atleast one video source is available
            if (videosources != null)
            {
                //For example use first video device. You may check if this is your webcam.
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);

                try
                {
                    //Check if the video device provides a list of supported resolutions
                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";
                        //Search for the highest resolution
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            if (videoSource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                                highestSolution = videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                        }
                        //Set the highest resolution as active
                        videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                    }
                }
                catch { }

                //Create NewFrame event handler
                //(This one triggers every time a new frame/image is captured
                videoSource.NewFrame += new Accord.Video.NewFrameEventHandler(Handler);


            }
        }
        /// <summary>
        /// Initial and Detect WebCam with Max Resolutain 
        /// </summary>
        public Webcam()
        {
            //ImageBox = Target;
            Runing = false;
            LastStatus = false;
            PictureSM = new Semaphore(1, 1);
            videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Check if atleast one video source is available
            if (videosources != null)
            {
                //For example use first video device. You may check if this is your webcam.
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);

                try
                {
                    //Check if the video device provides a list of supported resolutions
                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";
                        //Search for the highest resolution
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            if (videoSource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                                highestSolution = videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                        }
                        //Set the highest resolution as active
                        videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                    }
                }
                catch { }

                //Create NewFrame event handler
                //(This one triggers every time a new frame/image is captured
                //videoSource.NewFrame += new Accord.Video.NewFrameEventHandler(Handler);


            }
        }
        /// <summary>
        /// Initial and Detect WebCam with Spesific Resolutain 
        /// </summary>
        /// <param name="Width">Video and Picture Width</param>
        public Webcam(int Width)
        {
            Runing = false;
            LastStatus = false;
            //ImageBox = Target;
            PictureSM = new Semaphore(1, 1);
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Check if atleast one video source is available
            if (videosources != null)
            {
                //For example use first video device. You may check if this is your webcam.
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);

                try
                {
                    //Check if the video device provides a list of supported resolutions
                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = Width + ";0";
                        //Search for the highest resolution
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            if (videoSource.VideoCapabilities[i].FrameSize.Width == Convert.ToInt32(highestSolution.Split(';')[0]))
                                highestSolution = videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                        }
                        //Set the highest resolution as active
                        videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                    }
                }
                catch { }

                //Create NewFrame event handler
                //(This one triggers every time a new frame/image is captured
                videoSource.NewFrame += new Accord.Video.NewFrameEventHandler(videoSource_NewFrame);


            }
        }
        /// <summary>
        /// Start Use WebCam and Show Video
        /// </summary>
        public void StartWebCam()
        {
            Runing = true;
            //LastStatus = true;
            videoSource.Start();
        }
        /// <summary>
        /// Event taht Run when WebCam Create New Frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void videoSource_NewFrame(object sender, Accord.Video.NewFrameEventArgs eventArgs)
        {
            //Cast the frame as Bitmap object and don't forget to use ".Clone()" otherwise
            //you'll probably get access violation exceptions
            ((PictureBox)(ImageBox)).BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
        }
        /// <summary>
        /// Stop Show Video
        /// </summary>
        public void StopWebCam()
        {
            Runing = false;
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }
        /// <summary>
        /// Take Picture with WebCam
        /// </summary>
        /// <returns>WebCam Picture in Bitamap Format</returns>
        public void TakePicture(Action<object> CallBackFunc)
        {
            CallBackFunction = new Thread(new ParameterizedThreadStart(CallBackFunc));
            if (videoSource ==  null)
            {
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);
            }
            videoSource.NewFrame += VideoSource_Picture;
            LastStatus = videoSource.IsRunning;
            if (!videoSource.IsRunning)
            {
                StartWebCam();
                //Thread.Sleep(500);
                //if(LastStatus == false)
                //{
                //    videoSource.Stop();
                //}
                
            }
            PictureSM = new Semaphore(1, 1);
            //PictureSM.WaitOne();
            //PictureSM.WaitOne();
            //return Picture;
        }

        private void VideoSource_Picture(object sender, NewFrameEventArgs eventArgs)
        {
            if (LastStatus == false)
            {
                StopWebCam();
            }
            //videoSource.NewFrame -= VideoSource_Picture;
            Picture = (Bitmap)eventArgs.Frame.Clone();
            CallBackFunction.Start(Picture);
            //PictureSM.Release();
            //videoSource.Stop();
        }
    }
}
