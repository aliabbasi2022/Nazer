using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Microsoft.Win32;


namespace UI
{
    public class DriverLoaderWithService
    {
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr OpenSCManager(string MachineName, string DatabaseName, uint DesiredAccess);
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateService(IntPtr ServiceManager,string ServiceName,string DisplayName,uint DesiredAccess,
            uint ServiceType,uint StartType,uint ErrorControl,string BinaryPathName,string LoadOrderGroup,string TagId,string Dependencies,
            string ServiceStartName,string Password
        );
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr OpenService(IntPtr ServiceManager, string ServiceName, uint DesiredAccess);
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteService(IntPtr Service);
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseServiceHandle(IntPtr Handle);



        public enum ServiceStart : uint
        {
            /// <summary>
            /// A device driver started by the system loader. This value is valid
            /// only for driver services.
            /// </summary>
            ServiceBootStart = 0x00000000,
            /// <summary>
            /// A device driver started by the IoInitSystem function. This value 
            /// is valid only for driver services.
            /// </summary>
            ServiceSystemStart = 0x00000001,
            /// <summary>
            /// A service started automatically by the service control manager 
            /// during system startup. For more information, see Automatically 
            /// Starting Services.
            /// </summary>         
            ServiceAutoStart = 0x00000002,
            /// <summary>
            /// A service started by the service control manager when a process 
            /// calls the StartService function. For more information, see 
            /// Starting Services on Demand.
            /// </summary>
            ServiceDemandStart = 0x00000003,
            /// <summary>
            /// A service that cannot be started. Attempts to start the service
            /// result in the error code ERROR_SERVICE_DISABLED.
            /// </summary>
            ServiceDisabled = 0x00000004,
        }

        public enum AccessMask : uint
        {
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            SYNCHRONIZE = 0x00100000,
            STANDARD_RIGHTS_REQUIRED = 0x000F0000,
            STANDARD_RIGHTS_READ = 0x00020000,
            STANDARD_RIGHTS_WRITE = 0x00020000,
            STANDARD_RIGHTS_EXECUTE = 0x00020000,
            STANDARD_RIGHTS_ALL = 0x001F0000,
            SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
            ACCESS_SYSTEM_SECURITY = 0x01000000,
            MAXIMUM_ALLOWED = 0x02000000,
            GENERIC_READ = 0x80000000,
            GENERIC_WRITE = 0x40000000,
            GENERIC_EXECUTE = 0x20000000,
            GENERIC_ALL = 0x10000000,
            DESKTOP_READOBJECTS = 0x00000001,
            DESKTOP_CREATEWINDOW = 0x00000002,
            DESKTOP_CREATEMENU = 0x00000004,
            DESKTOP_HOOKCONTROL = 0x00000008,
            DESKTOP_JOURNALRECORD = 0x00000010,
            DESKTOP_JOURNALPLAYBACK = 0x00000020,
            DESKTOP_ENUMERATE = 0x00000040,
            DESKTOP_WRITEOBJECTS = 0x00000080,
            DESKTOP_SWITCHDESKTOP = 0x00000100,
            WINSTA_ENUMDESKTOPS = 0x00000001,
            WINSTA_READATTRIBUTES = 0x00000002,
            WINSTA_ACCESSCLIPBOARD = 0x00000004,
            WINSTA_CREATEDESKTOP = 0x00000008,
            WINSTA_WRITEATTRIBUTES = 0x00000010,
            WINSTA_ACCESSGLOBALATOMS = 0x00000020,
            WINSTA_EXITWINDOWS = 0x00000040,
            WINSTA_ENUMERATE = 0x00000100,
            WINSTA_READSCREEN = 0x00000200,
            WINSTA_ALL_ACCESS = 0x0000037F,
            SECTION_QUERY = 0x0001,
            SECTION_MAP_WRITE = 0x0002,
            SECTION_MAP_READ = 0x0004,
            SECTION_MAP_EXECUTE = 0x0008,
            SECTION_EXTEND_SIZE = 0x0010,
            SECTION_READ_WRITE = SECTION_MAP_READ | SECTION_MAP_WRITE,
            SECTION_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | SECTION_QUERY | SECTION_MAP_WRITE | SECTION_MAP_READ | SECTION_MAP_EXECUTE | SECTION_EXTEND_SIZE,
            EVENT_QUERY_STATE = 0x0001,
            EVENT_MODIFY_STATE = 0x0002,
            EVENT_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0x3,
        }

        public enum ServiceAccess : uint
        {
            /// <summary>
            /// Required to call the QueryServiceConfig and 
            /// QueryServiceConfig2 functions to query the service configuration.
            /// </summary>
            ServiceQueryConfig = 0x00001,
            /// <summary>
            /// Required to call the ChangeServiceConfig or ChangeServiceConfig2 function 
            /// to change the service configuration. Because this grants the caller 
            /// the right to change the executable file that the system runs, 
            /// it should be granted only to administrators.
            /// </summary>
            ServiceChangeConfig = 0x00002,
            /// <summary>
            /// Required to call the QueryServiceStatusEx function to ask the service 
            /// control manager about the status of the service.
            /// </summary>
            ServiceQueryStatus = 0x00004,
            /// <summary>
            /// Required to call the EnumDependentServices function to enumerate all 
            /// the services dependent on the service.
            /// </summary>
            ServiceEnumerateDependents = 0x00008,
            /// <summary>
            /// Required to call the StartService function to start the service.
            /// </summary>
            ServiceStart = 0x00010,
            /// <summary>
            ///     Required to call the ControlService function to stop the service.
            /// </summary>
            ServiceStop = 0x00020,
            /// <summary>
            /// Required to call the ControlService function to pause or continue 
            /// the service.
            /// </summary>
            ServicePauseContinue = 0x00040,
            /// <summary>
            /// Required to call the EnumDependentServices function to enumerate all
            /// the services dependent on the service.
            /// </summary>
            ServiceInterrogate = 0x00080,
            /// <summary>
            /// Required to call the ControlService function to specify a user-defined
            /// control code.
            /// </summary>
            ServiceUserDefinedControl = 0x00100,
            /// <summary>
            /// Includes STANDARD_RIGHTS_REQUIRED in addition to all access rights in this table.
            /// </summary>
            ServiceAllAccess = (AccessMask.STANDARD_RIGHTS_REQUIRED |
                ServiceAccess.ServiceQueryConfig |
                ServiceAccess.ServiceChangeConfig |
                ServiceAccess.ServiceQueryStatus |
                ServiceAccess.ServiceEnumerateDependents |
                ServiceAccess.ServiceStart |
                ServiceAccess.ServiceStop |
                ServiceAccess.ServicePauseContinue |
                ServiceAccess.ServiceInterrogate |
                ServiceAccess.ServiceUserDefinedControl |
                ServiceAccess.ReadControl |
                ServiceAccess.GenericWrite |
                ServiceAccess.Delete),
           /// <summary>
            /// Includes STANDARD_RIGHTS_REQUIRED in addition to all access rights in this table.
            /// </summary>
            QueryStartStopDelete = (AccessMask.STANDARD_RIGHTS_REQUIRED |
                ServiceAccess.ServiceQueryConfig |
                ServiceAccess.ServiceChangeConfig |
                ServiceAccess.ServiceQueryStatus |
                ServiceAccess.ServiceStart |
                ServiceAccess.ServiceStop |
                ServiceAccess.Delete),
            GenericRead = AccessMask.STANDARD_RIGHTS_READ |
                ServiceAccess.ServiceQueryConfig |
                ServiceAccess.ServiceQueryStatus |
                ServiceAccess.ServiceInterrogate |
                ServiceAccess.ServiceEnumerateDependents,
            GenericWrite = AccessMask.STANDARD_RIGHTS_WRITE |
                ServiceAccess.ServiceChangeConfig,
            GenericExecute = AccessMask.STANDARD_RIGHTS_EXECUTE |
                ServiceAccess.ServiceStart |
                ServiceAccess.ServiceStop |
                ServiceAccess.ServicePauseContinue |
                ServiceAccess.ServiceUserDefinedControl,
            /// <summary>
            /// Required to call the QueryServiceObjectSecurity or 
            /// SetServiceObjectSecurity function to access the SACL. The proper
            /// way to obtain this access is to enable the SE_SECURITY_NAME 
            /// privilege in the caller's current access token, open the handle 
            /// for ACCESS_SYSTEM_SECURITY access, and then disable the privilege.
            /// </summary>
            AccessSystemSecurity = AccessMask.ACCESS_SYSTEM_SECURITY,
            /// <summary>
            /// Required to call the DeleteService function to delete the service.
            /// </summary>
            Delete = AccessMask.DELETE,
            /// <summary>
            /// Required to call the QueryServiceObjectSecurity function to query
            /// the security descriptor of the service object.
            /// </summary>
            ReadControl = AccessMask.READ_CONTROL,
            /// <summary>
            /// Required to call the SetServiceObjectSecurity function to modify
            /// the Dacl member of the service object's security descriptor.
            /// </summary>
            WriteDac = AccessMask.WRITE_DAC,
            /// <summary>
            /// Required to call the SetServiceObjectSecurity function to modify 
            /// the Owner and Group members of the service object's security 
            /// descriptor.
            /// </summary>
            WriteOwner = AccessMask.WRITE_OWNER,
        }

        public enum ScmAccess : uint
        {
            StandardRightsRequired = 0xF0000,
            ScManagerConnect = 0x00001,
            ScManagerCreateService = 0x00002,
            ScManagerEnumerateService = 0x00004,
            ScManagerLock = 0x00008,
            ScManagerQueryLockStatus = 0x00010,
            ScManagerModifyBootConfig = 0x00020,
            ScManagerAllAccess = ScmAccess.StandardRightsRequired |
                                          ScmAccess.ScManagerConnect |
                                          ScmAccess.ScManagerCreateService |
                                          ScmAccess.ScManagerEnumerateService |
                                          ScmAccess.ScManagerLock |
                                          ScmAccess.ScManagerQueryLockStatus |
                                          ScmAccess.ScManagerModifyBootConfig
        }

        public enum ServiceError
        {
            /// <summary>
            /// The startup program ignores the error and continues the startup
            /// operation.
           /// </summary>
            ServiceErrorIgnore = 0x00000000,
            /// <summary>
            /// The startup program logs the error in the event log but continues
            /// the startup operation.
            /// </summary>
            ServiceErrorNormal = 0x00000001,
            /// <summary>
            /// The startup program logs the error in the event log. If the 
            /// last-known-good configuration is being started, the startup 
            /// operation continues. Otherwise, the system is restarted with 
            /// the last-known-good configuration.
            /// </summary>
            ServiceErrorSevere = 0x00000002,
            /// <summary>
            /// The startup program logs the error in the event log, if possible.
            /// If the last-known-good configuration is being started, the startup
            /// operation fails. Otherwise, the system is restarted with the 
            /// last-known good configuration.
            /// </summary>
            ServiceErrorCritical = 0x00000003,
        }

        public enum ServiceType : uint
        {
            /// <summary>
            /// Driver service.
            /// </summary>
            ServiceKernelDriver = 0x00000001,
            /// <summary>
            /// File system driver service.
            /// </summary>
            ServiceFileSystemDriver = 0x00000002,
            /// <summary>
            /// Service that runs in its own process.
            /// </summary>
            ServiceWin32OwnProcess = 0x00000010,
            /// <summary>
            /// Service that shares a process with one or more other services.
            /// </summary>
            ServiceWin32ShareProcess = 0x00000020,
            /// <summary>
            /// The service can interact with the desktop.
            /// </summary>
            ServiceInteractiveProcess = 0x00000100,
        }


        /// <summary>
        /// Creates the specified service.
        /// </summary>
        /// <param name="ServiceName">Name of the service.</param>
        /// <param name="DisplayName">The display name.</param>
        /// <param name="ServiceAccess">The service access.</param>
        /// <param name="ServiceType">Type of the service.</param>
        /// <param name="ServiceStart">The service start.</param>
        /// <param name="ServiceError">The service error.</param>
        /// <param name="File">The file.</param>

        public  static IntPtr Create(string ServiceName, string DisplayName, ServiceAccess ServiceAccess, ServiceType ServiceType, ServiceStart ServiceStart, ServiceError ServiceError, FileInfo File)
        {
            IntPtr ServiceManager = OpenSCManager(null, null, (uint)ScmAccess.ScManagerAllAccess);
            if (ServiceManager == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }
            IntPtr Service = CreateService(ServiceManager,ServiceName,DisplayName,(uint)ServiceAccess,(uint)ServiceType,
                (uint)ServiceStart,(uint)ServiceError,File.FullName,null, null, null, null, null);
            CloseServiceHandle(ServiceManager);
            if (Service == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }
            return Service;
        }


        /// <summary>
        /// Opens the specified service.
        /// </summary>
        /// <param name="ServiceName">Name of the service.</param>
        /// <param name="ServiceAccess">The service access.</param>

        public  static IntPtr Open(string ServiceName, ServiceAccess ServiceAccess)
        {
            var ServiceManager = OpenSCManager(null, null, (uint)ScmAccess.ScManagerAllAccess);
            if (ServiceManager != IntPtr.Zero)
            {
                var Handle = OpenService(ServiceManager, ServiceName, (uint)ServiceAccess);
                if (!Close(ServiceManager))
                {
                    // ..
                }
                return Handle;
            }
            return IntPtr.Zero;
        }



        /// <summary>
        /// Creates or opens the specified service.
        /// </summary>
        /// <param name="ServiceName">Name of the service.</param>
        /// <param name="DisplayName">The display name.</param>
        /// <param name="ServiceAccess">The service access.</param>
        /// <param name="ServiceType">Type of the service.</param>
        /// <param name="ServiceStart">The service start.</param>
        /// <param name="ServiceError">The service error.</param>
        /// <param name="File">The file.</param>

        public  static IntPtr CreateOrOpen(string ServiceName, string DisplayName, ServiceAccess ServiceAccess, ServiceType ServiceType, ServiceStart ServiceStart, ServiceError ServiceError, FileInfo File)
        {
            var Handle = Create(ServiceName, DisplayName, ServiceAccess, ServiceType, ServiceStart, ServiceError, File);
            if (Handle == IntPtr.Zero)
            {
                return Open(ServiceName, ServiceAccess);
            }
            return Handle;
        }

        /// <summary>
        /// Checks if a service exist.
        /// </summary>
        /// <param name="ServiceName">The service name.</param>

        internal static bool Exists(string ServiceName)
        {
            var Handle = Open(ServiceName, ServiceAccess.ServiceAllAccess);
            if (Handle != IntPtr.Zero)
            {
                if (!Close(Handle))
                {
                    // ..
                }
                return true;
            }
            return false;
        }



        /// <summary>
        /// Checks if a service exist using the specified comparer.
        /// </summary>
        /// <param name="ServiceName">The service name.</param>
        /// <param name="Comparer">The comparer.</param>

        public static bool ExistsInRegistry(string ServiceName, Func<ServiceController, bool> Comparer = null)
        {
            if (Comparer != null)
            {
               var Services = ServiceController.GetServices();
                if (Services.Any(Comparer))
                {
                    return true;
                }
            }
            using (var Regedit = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\",RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.EnumerateSubKeys))
            {
                var Names = Regedit.GetSubKeyNames();
                if (Names.Any(Name => Name == ServiceName))
                {
                    return true;
                }
            }
            return false;
        }



        /// <summary>
        /// Deletes the specified service.
        /// </summary>
        /// <param name="Handle">The handle.</param>
        /// <exception cref="ArgumentException">Handle is invalid at Delete(Handle). - Handle</exception>
        
        public  static bool Delete(IntPtr Handle)
        {
            if (Handle == IntPtr.Zero)
            {
                throw new ArgumentException("Handle is invalid at Delete(Handle).", nameof(Handle));
            }
            if (!DeleteService(Handle))
            {
                return false;
            }
            if (!Close(Handle))
            {
                // ..
            }
            return true;
        }



        /// <summary>
        /// Closes the specified service handle.
        /// </summary>
        /// <param name="Handle">The handle.</param>

        public static bool Close(IntPtr Handle)
        {
            return CloseServiceHandle(Handle);
        }
    }
}
