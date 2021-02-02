using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Diagnostics;
using System.Windows.Forms;

namespace Child
{
    class Browsers
    {
        // For IE Change Title Event 
        public List<AutomationElement> ElementSubscribePage;
        public List<AutomationPropertyChangedEventHandler> UIAeventHandler;
        /* --------------- Set Event On Title Page -----------------
         // When Page Content is Change Event Called;
         // When Page Content Is Change , Page Title is Change , When We Set Event to Watch to the Title Page 
         //we Can Find out When Page change and handler them */

        public Browsers()
        {
            UIAeventHandler = new List<AutomationPropertyChangedEventHandler>(5);
            ElementSubscribePage = new List<AutomationElement>(5);
        }
        /// <summary>
        /// SubscribeToInvoke PropertyChangedEventHandler.
        /// Set Event in to Name Property on the Page Parameter to find out when 
        /// </summary>
        /// <param name="Page"></param>
        public void SubscribeToInvoke(AutomationElement Page, AutomationProperty Property,ref AutomationPropertyChangedEventHandler EventHandlerFunction)
        {
            if (Page != null)
            {
                
                UIAeventHandler.Add( new AutomationPropertyChangedEventHandler(EventHandlerFunction));
                Automation.AddAutomationPropertyChangedEventHandler(
                     Page, TreeScope.Element, UIAeventHandler[UIAeventHandler.Count - 1]
                     , Property);
                ElementSubscribePage.Add(Page);
            }

        }


        //------For chrome Browser -------------------
        //------ Start -----------------------

        public void Chrome(AutomationPropertyChangedEventHandler ChromeHandler)
        {
            Process[] ChromProcess = Process.GetProcessesByName("chrome");// Find All chrome Procces 
            foreach (Process P in ChromProcess)
            {
                if ((P.MainWindowHandle.ToInt32() > 0) && (P.MainWindowTitle != ""))/* Check Active Procces to Find Usage Page . when MainWindowHandle.ToInt32()
                                                                                     More than 0 , Page is Active and we can use to find URL
                                                                                     When Page Have Valid MainWindowHandle.ToInt32() but Not Trust page 
                                                                                     Becuse use MainWindowTitle to Find Trust Page */
                {
                    AutomationElement Page = AutomationElement.FromHandle(P.MainWindowHandle);// Get chrome Main Window From Pointer 
                    SubscribeToInvoke(Page, AutomationElement.NameProperty,ref ChromeHandler );// Set Event To Name Property ,to Find when Page has change 
                }

            }
        }

        /* Event Handler for when Chrome Page Name Has Change 
        this Event must use out the this Class*/
        public void ChromeHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElement EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"))[0];// find Edit box that 
            ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
            string URL = Value.Current.Value;
        }
        //----------- End--------------------

        //-------------- For firefox Browser -------------------
        //-------------- Start -----------------------
        public void Redirect(AutomationElement element, string value)
        {
            try
            {
                // Once you have an instance of an AutomationElement,  
                // check if it supports the ValuePattern pattern.
                object valuePattern = null;

                /* Control does not support the ValuePattern pattern 
                 so use keyboard input to insert content.
                 NOTE: Elements that support TextPattern 
                       do not support ValuePattern and TextPattern
                       does not support setting the text of 
                       multi-line edit or document controls.
                       For this reason, text input must be simulated
                       using one of the following methods.*/
                       
                if (!element.TryGetCurrentPattern(
                    ValuePattern.Pattern, out valuePattern))
                {
                  
                    // Set focus for input functionality and begin.
                    element.SetFocus();

                    /* Pause before sending keyboard input.
                     Delete existing content in the control and insert new content.*/
                    SendKeys.SendWait("^{HOME}");   // Move to start of control
                    SendKeys.SendWait("^+{END}");   // Select everything
                    SendKeys.SendWait("{DEL}");     // Delete selection
                }
                /* Control supports the ValuePattern pattern so we can 
                  use the SetValue method to insert content.*/
                else
                {
                    // Set focus for input functionality and begin.
                    element.SetFocus();

                    ((ValuePattern)valuePattern).SetValue(value);
                    SendKeys.SendWait("{ENTER}");
                }
            }
            catch (ArgumentNullException exc)
            {
                
            }
            catch (InvalidOperationException exc)
            {
                
            }
            finally
            {
                
            }
        }
        public void FireFox(AutomationPropertyChangedEventHandler firefoxHandler)
        {
            Process[] ChromProcess = Process.GetProcessesByName("firefox");// Find All firefox Procces 
            foreach (Process P in ChromProcess)
            {
                if ((P.MainWindowHandle.ToInt32() > 0) && (P.MainWindowTitle != ""))/* Check Active Procces to Find Usage Page . when MainWindowHandle.ToInt32()
                                                                                     More than 0 , Page is Active and we can use to find URL
                                                                                     When Page Have Valid MainWindowHandle.ToInt32() but Not Trust page 
                                                                                     Becuse use MainWindowTitle to Find Trust Page*/ 
                {
                    AutomationElement Page = AutomationElement.FromHandle(P.MainWindowHandle);// Get firefox Main Window From Pointer 
                    SubscribeToInvoke(Page, AutomationElement.NameProperty,ref firefoxHandler );// Set Event To Name Property ,to Find when Page has change 
                }

            }
        }

        /* Event Handler for when firefox Page Name Has Change 
        this Event must use out the this Class*/
        public void firefoxHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElement EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Search or enter address"))[0];// find Edit box that 
            // Name is "Search or enter address"
            ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
            string URL = Value.Current.Value;
        }

        //--------------- End -----------------

        //------ For IE Browser -------------------
        //------ Start -----------------------

        public void IE(AutomationPropertyChangedEventHandler IEHandler)
        {
            Process[] IEProcces = Process.GetProcessesByName("iexplore");// Find All IE Procces 
            foreach (Process P in IEProcces)
            {
                if ((P.MainWindowHandle.ToInt32() > 0) && (P.MainWindowTitle != ""))/* Check Active Procces to Find Usage Page . when MainWindowHandle.ToInt32()
                                                                                     More than 0 , Page is Active and we can use to find URL
                                                                                     When Page Have Valid MainWindowHandle.ToInt32() but Not Trust page 
                                                                                     Becuse use MainWindowTitle to Find Trust Page*/ 
                {
                    AutomationElement Page = AutomationElement.FromHandle(P.MainWindowHandle); // Get IE Main Window From Pointer 
                    SubscribeToInvoke(Page, AutomationElement.NameProperty,ref IEHandler );// Set Event To Name Property ,to Find when Page has change 
                }

            }
        }

        //this Event must use out the this Class
        public void IEHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElement EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search using Bing"))[0];// find Edit box that 
            // Name is "Address and search using Bing"
            ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
            string URL = Value.Current.Value;
        }


        //----------- End--------------------

        //---------------- For Opera Browser -------------------
        //---------------- Start -----------------------

        public void Opera(AutomationPropertyChangedEventHandler OperaHandler)
        {
            Process[] ChromProcess = Process.GetProcessesByName("opera");// Find All opera Procces 
            foreach (Process P in ChromProcess)
            {
                if ((P.MainWindowHandle.ToInt32() > 0) && (P.MainWindowTitle != ""))// Check Active Procces to Find Usage Page . when MainWindowHandle.ToInt32()
                                                                                    // More than 0 , Page is Active and we can use to find URL
                                                                                    // When Page Have Valid MainWindowHandle.ToInt32() but Not Trust page 
                                                                                    //Becuse use MainWindowTitle to Find Trust Page 
                {
                    AutomationElement Page = AutomationElement.FromHandle(P.MainWindowHandle);// Get opera Main Window From Pointer 
                    SubscribeToInvoke(Page, AutomationElement.NameProperty,ref OperaHandler );// Set Event To Name Property ,to Find when Page has change 
                }

            }
        }

        /* Event Handler for when opera Page Name Has Change 
         this Event must use out the this Class*/

        public void OperaHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElement EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address field"))[0];// find Edit box that 
            // Name is "Address field"
            ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
            string URL = Value.Current.Value;
        }

        //----------- End --------------------


        //---------------- For Edge Browser -------------------
        //---------------- Start -----------------------

        public void Edge(AutomationPropertyChangedEventHandler EdgeHandler)
        {
            Process[] All = Process.GetProcesses();
            Process[] EdgeProcess = Process.GetProcessesByName("ApplicationFrameHost");// Find All Edge Procces 
            foreach (Process P in EdgeProcess)
            {
                if ((P.MainWindowHandle.ToInt32() > 0) && (P.MainWindowTitle != ""))/* Check Active Procces to Find Usage Page . when MainWindowHandle.ToInt32()
                                                                                     More than 0 , Page is Active and we can use to find URL
                                                                                     When Page Have Valid MainWindowHandle.ToInt32() but Not Trust page 
                                                                                     Becuse use MainWindowTitle to Find Trust Page */
                {
                    AutomationElement Page = AutomationElement.FromHandle(P.MainWindowHandle);// Get Edge Main Window From Pointer 
                    SubscribeToInvoke(Page, AutomationElement.NameProperty,ref EdgeHandler );// Set Event To Name Property ,to Find when Page has change 
                }

            }
        }


        /* Event Handler for when Edge Page Name Has Change 
        this Event must use out the this Class*/
        public void EdgeHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElementCollection EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, PropertyCondition.TrueCondition);/* find Edit box that 
                                                                                           Name is "Address and search bar"
                                                                                           ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
                                                                                           string URL = Value.Current.Value;*/
        }
        //----------------- END -----------------
    }
}
