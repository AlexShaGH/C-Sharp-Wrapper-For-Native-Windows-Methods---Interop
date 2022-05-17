using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Interop
{
    public class NativeMethods
    {

        #region Structures

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct Win32FindDataStruct
        {
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Win32FileAttributeDataStruct
        {
            public FileAttributes dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Win32FILETIME
        {
            public uint dwLowDateTime;
            public uint dwHighDateTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA
        {
            public UInt32 cbSize;
            public Guid ClassGuid;
            public UInt32 DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVICE_INTERFACE_DATA
        {
            public uint cbSize;
            public Guid InterfaceClassGuid;
            public uint Flags;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STORAGE_DEVICE_NUMBER
        {
            public UInt32 DeviceType;
            public UInt32 DeviceNumber;
            public UInt32 PartitionNumber;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ATA_PASS_THROUGH_DIRECT
        {
            public UInt16 Length;
            public UInt16 AtaFlags;
            public Byte PathId;
            public Byte TargetId;
            public Byte Lun;
            public Byte ReservedAsUchar;
            public UInt32 DataTransferLength;
            public UInt32 TimeOutValue;
            public UInt32 ReservedAsUlong;
            public IntPtr DataBuffer;
            public UInt64 PreviousTaskFile;
            public UInt64 CurrentTaskFile;
            /*
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] PreviousTaskFile;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] CurrentTaskFile; */
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SCSI_PASS_THROUGH_DIRECT
        {
            public UInt16 Length;
            public Byte ScsiStatus;
            public Byte PathId;
            public Byte TargetId;
            public Byte Lun;
            public Byte CdbLength;
            public Byte SenseInfoLength;
            public Byte DataIn;
            public UInt32 DataTransferLength;
            public UInt32 TimeOutValue;
            public IntPtr DataBuffer;
            public UInt32 SenseInfoOffset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] Cdb;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SAT_SPTIoContext
        {
            public Int64 Padding0;
            public SCSI_PASS_THROUGH_DIRECT Sptd;
            public Int64 Padding1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 252)]
            public byte[] SenseBuffer;
            public UInt32 SptBufLen;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STORAGE_PROPERTY_QUERY
        {
            public Int32 PropertyId;
            public Int32 QueryType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] AdditionalParameters;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STORAGE_DEVICE_DESCRIPTOR
        {
            public UInt32 Version;
            public UInt32 Size;
            public byte DeviceType;
            public byte DeviceTypeModifier;
            public byte RemovableMedia;
            public byte CommandQueueing;
            public UInt32 VendorIdOffset;
            public UInt32 ProductIdOffset;
            public UInt32 ProductRevisionOffset;
            public UInt32 SerialNumberOffset;
            public uint BusType;
            public UInt32 RawPropertiesLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048)]
            public byte[] RawDeviceProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct USB_MEDIA_SERIAL_NUMBER_DATA
        {
            public UInt32 SerialNumberLength;
            public UInt32 Result;
            public UInt64 Reserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public byte[] SerialNumberData;
        }

        #endregion

        #region Enumerations

        public enum GetFileExInfoLevelsEnum
        {
            GetFileExInfoStandard,
            GetFileExMaxInfoLevel
        }

        public enum FindExSearchOpsEnum
        {
            FindExSearchNameMatch,
            FindExSearchLimitToDirectories,
            FindExSearchLimitToDevices
        }

        public enum FindExInfoLevelsEnum
        {
            FindExInfoStandard,
            FindExInfoBasic,
            FindExInfoMaxInfoLevel
        }

        public enum EFileAccess : uint
        {
            GenericRead = 0x80000000,
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000,
        }
        public enum EFileShare : uint
        {
            None = 0x00000000,
            Read = 0x00000001,
            Write = 0x00000002,
            Delete = 0x00000004,
        }
        public enum ECreationDisposition : uint
        {
            New = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5,
        }
        public enum EFileAttributes : uint
        {
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotContentIndexed = 0x00002000,
            Encrypted = 0x00004000,
            Write_Through = 0x80000000,
            Overlapped = 0x40000000,
            NoBuffering = 0x20000000,
            RandomAccess = 0x10000000,
            SequentialScan = 0x08000000,
            DeleteOnClose = 0x04000000,
            BackupSemantics = 0x02000000,
            PosixSemantics = 0x01000000,
            OpenReparsePoint = 0x00200000,
            OpenNoRecall = 0x00100000,
            FirstPipeInstance = 0x00080000
        }
        public enum DiGetClassFlags : uint
        {
            DIGCF_DEFAULT = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE
            DIGCF_PRESENT = 0x00000002,
            DIGCF_ALLCLASSES = 0x00000004,
            DIGCF_PROFILE = 0x00000008,
            DIGCF_DEVICEINTERFACE = 0x00000010,
        }

        public enum DriveType : uint
        {
            Unknown = 0,    //DRIVE_UNKNOWN
            Error = 1,      //DRIVE_NO_ROOT_DIR
            Removable = 2,  //DRIVE_REMOVABLE
            Fixed = 3,      //DRIVE_FIXED
            Remote = 4,     //DRIVE_REMOTE
            CDROM = 5,      //DRIVE_CDROM
            RAMDisk = 6     //DRIVE_RAMDISK
        }

        #endregion

        #region Methods

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetFileAttributes(string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetFileAttributes(string lpFileName, uint dwFileAttributes);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetShortPathName(string lpszLongPath, StringBuilder lpszShortPath, uint cchBuffer);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindFirstFile(String lpFileName, out Win32FindDataStruct lpFindFileData);
        //public static extern SafeFileHandle FindFirstFile(String lpFileName, out Win32FindDataStruct lpFindFileData); //Requires to implement a wrapper class of SafeHandle

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool FindNextFile(IntPtr hFindFile, out Win32FindDataStruct lpFindFileData);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //public static extern bool FindNextFile(SafeFileHandle hFindFile, out Win32FindDataStruct lpFindFileData); //Requires to implement a wrapper class of SafeHandle

        [DllImport("kernel32.dll")]
        public static extern bool FindClose(IntPtr hFindFile);

        //[DllImport("kernel32.dll")]
        //public static extern bool FindClose(SafeFileHandle hFindFile); //Requires to implement a wrapper class of SafeHandle

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetFileAttributesEx(string lpFileName,
           GetFileExInfoLevelsEnum fInfoLevelId, ref Win32FileAttributeDataStruct lpFileInformation);

        [DllImport("kernel32.dll")]
        public static extern IntPtr FindFirstFileEx(string lpFileName, FindExInfoLevelsEnum
           fInfoLevelId, IntPtr lpFindFileData, FindExSearchOpsEnum fSearchOp,
           IntPtr lpSearchFilter, uint dwAdditionalFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetFileTime(SafeFileHandle hFile,
            ref Win32FILETIME ftCreationTime,
            ref Win32FILETIME ftLastAccessTime,
            ref Win32FILETIME ftLastWriteTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetFileTime(SafeFileHandle hFile,
            ref Win32FILETIME ftCreationTime,
            ref Win32FILETIME ftLastAccessTime,
            ref Win32FILETIME ftLastWriteTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetFileTime(SafeFileHandle hFile, IntPtr ftCreationTime, IntPtr ftLastAccessTime, IntPtr ftLastWriteTime);

        [DllImport("kernel32.dll", EntryPoint = "CreateSymbolicLinkW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int CreateSymbolicLink([In] string lpSymlinkFileName, [In] string lpTargetFileName, int dwFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DeleteFile(string path);
        
        [DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            EFileAccess dwDesiredAccess,
            EFileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            ECreationDisposition dwCreationDisposition,
            EFileAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(
            SafeFileHandle hDevice,
            uint dwIoControlCode,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            IntPtr lpOutBuffer,
            uint nOutBufferSize,
            ref uint lpBytesReturned,
            IntPtr lpOverlapped); //[In] ref NativeOverlapped lpOverlapped);

        [DllImport("Kernel32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(
            SafeFileHandle hDevice,
            uint dwIoControlCode,
            [MarshalAs(UnmanagedType.AsAny)]
            [In] object InBuffer,
            uint nInBufferSize,
            [MarshalAs(UnmanagedType.AsAny)]
            [Out] object OutBuffer,
            uint nOutBufferSize,
            ref uint pBytesReturned,
            IntPtr lpOverlapped );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetupDiGetClassDevs(
            ref Guid ClassGuid,
            IntPtr Enumerator,
            IntPtr hwndParent,
            uint Flags);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList
        (
             IntPtr DeviceInfoSet
        );

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiEnumDeviceInterfaces(
            IntPtr DeviceInfoSet,
            IntPtr DeviceInfoData,
            ref Guid InterfaceClassGuid,
            UInt32 MemberIndex,
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(
            IntPtr DeviceInfoSet,
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
            IntPtr DeviceInterfaceDetailData,
            UInt32 DeviceInterfaceDetailDataSize,
            out UInt32 RequiredSize,
            IntPtr DeviceInfoData); //ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("kernel32.dll")]
        public static extern DriveType GetDriveType(string lpRootPathName); //A trailing backslash is required

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetVolumeInformation(
            string RootPathName,
            StringBuilder VolumeNameBuffer,
            int VolumeNameSize,
            IntPtr VolumeSerialNumber,
            IntPtr MaximumComponentLength,
            IntPtr FileSystemFlags,
            StringBuilder FileSystemNameBuffer,
            int nFileSystemNameSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void RtlFillMemory(IntPtr destination, uint length, byte fill);

        [DllImport("kernel32.dll")]
        public static extern uint SetErrorMode(uint uMode);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CreateDirectory(string lpPathName, IntPtr lpSecurityAttributes);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CopyFile(string lpExistingFileName, string lpNewFileName, bool bFailIfExists);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool MoveFile(string lpExistingFileName, string lpNewFileName);


        //
        // Helper functions using native Win32 APIs, naming prefixed by 'H'. Throwable.
        //
        public static String HSortPathFileName(String dirFileName)
        {
            if (String.IsNullOrEmpty(dirFileName))
                return "";

            String name = dirFileName;
            while (name.StartsWith(@"\\?\"))
                name = name.Remove(0, 4);

            name = name.Replace('/', '\\');

            if (name.StartsWith(@"\\") && name.Length > 2)
                name = "UNC" + name.Substring(1); //prepare for "UNC\server\path"

            while (name.Contains(@"\\"))
                name = name.Replace(@"\\", @"\");

            if (name.Contains(":"))
            {
                if(name.Length == 2) //"C:" => "C:\"
                    name = name + "\\";
                else if(name.Length > 3)
                    name = name.TrimEnd('\\');
            }
            else
                name = name.TrimEnd('\\');

            if( name != "" )
                name = @"\\?\" + name;

            return name;
        } //--HSortPathFileName

        public static bool HDirectoryFileExists(String dirFileName)
        {
            String longName = HSortPathFileName(dirFileName);
            if (longName == "")
                return false; //throw new System.Exception("Directory path is empty");

            if (GetFileAttributes(longName) == -1) //INVALID_FILE_ATTRIBUTES
            {
                int err = Marshal.GetLastWin32Error();
                if (2 == err || 3 == err || 21 == err) //ERROR_FILE_NOT_FOUND, ERROR_PATH_NOT_FOUND, ERROR_NOT_READY (CD/DVD)
                    return false;
                else
                    return false; //throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
            return true;

            /*
            //Note: Win32 API GetFileAttributes() has limitations and FindFirstFile() may fit better to match .NET Directory.Exists()/File.Exists().
            NativeMethods.Win32FindDataStruct w32FindDtStruct = new NativeMethods.Win32FindDataStruct();
            //IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(w32FindDtStruct));
            //Marshal.StructureToPtr(w32FindDtStruct, ptr, true);
            SafeFileHandle safeFileHandle = NativeMethods.FindFirstFile(longName, out w32FindDtStruct);
            if (safeFileHandle.IsInvalid)
            {
                int err = Marshal.GetLastWin32Error();
                if (2 == err || 3 == err) //ERROR_FILE_NOT_FOUND, ERROR_PATH_NOT_FOUND
                    return false;
                else
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                NativeMethods.FindClose(safeFileHandle);
                return true;
            }
            */
        } //--HDirectoryFileExists

        public static void HCreateMultiLevelDirectory(String dirName)
        {
            if (String.IsNullOrEmpty(dirName))
                throw new System.Exception("Path of directory to be created is empty");

            String path = "";
            String longName = HSortPathFileName(dirName);
            if (longName == "")
                throw new System.Exception("Directory path is empty"); //return;

            if(longName.StartsWith(@"\\?\UNC\"))
            {
                int ii = longName.IndexOf( '\\', 8);
                if (-1 == ii)
                {
                    path = longName; //"\\?\UNC\server"
                    longName = "";
                }
                else
                {
                    path = longName.Substring(0, ii); //"\\?\UNC\server"
                    longName = longName.Substring(ii);
                }
            }
            else
                longName = longName.Substring(4);

            string[] subDirs = longName.Split('\\');
            foreach (string subDir in subDirs)
            {
                if (subDir == "")
                    continue;

                if(subDir.EndsWith(":"))
                {
                    path = @"\\?\" + subDir; //Drive letter, eg. "C:"
                    continue;
                }

                if (path == "")
                    path = @"\\?\" + subDir;
                else
                    path = path + "\\" + subDir;

                if (HDirectoryFileExists(path))
                    continue;

                if (!CreateDirectory(path, IntPtr.Zero))
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
        } //--HCreateMultiLevelDirectory

        public static int HGetDirFileAttributes(String dirFileName)
        {
            String longName = HSortPathFileName(dirFileName);
            if (longName == "")
                throw new System.Exception("Directory path or file name to get attributes is empty");

            int attr = GetFileAttributes(longName);
            if( -1 == attr )
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            return attr;
        } //--HGetDirFileAttributes

        public static void HSetDirFileAttributes(String dirFileName, uint dwAttributes)
        {
            String longName = HSortPathFileName(dirFileName);
            if (longName == "")
                throw new System.Exception("Directory path or file name to set attributes is empty");

            if(!SetFileAttributes(longName, dwAttributes))
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        } //--HGetDirFileAttributes

        public static void HCopyFile(String lpExistingFileName, String lpNewFileName, bool bFailIfExists)
        {
            String existingName = HSortPathFileName(lpExistingFileName);
            if (existingName == "")
                throw new System.Exception("File name of source to copy is empty");

            String newName = HSortPathFileName(lpNewFileName);
            if (newName == "")
                throw new System.Exception("File name of destination to copy is empty");

            if(!CopyFile(existingName, newName, bFailIfExists))
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        } //--HCopyFile

        public static void HMoveFile(String lpExistingFileName, String lpNewFileName)
        {
            String existingName = HSortPathFileName(lpExistingFileName);
            if (existingName == "")
                throw new System.Exception("File name of source to move is empty");

            String newName = HSortPathFileName(lpNewFileName);
            if (newName == "")
                throw new System.Exception("File name of destination to move is empty");

            if (!MoveFile(existingName, newName))
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        } //--HMoveFile

        public static void HSetDirFileTime(String dirFileName, DateTime? ftCreationTime, DateTime? ftLastAccessTime, DateTime? ftLastWriteTime)
        {
            String longName = HSortPathFileName(dirFileName);
            if (longName == "")
                throw new System.Exception("Directory path or file name to set time-stamps is empty");

            SafeFileHandle handle = CreateFile(longName,
                                            NativeMethods.EFileAccess.GenericRead | NativeMethods.EFileAccess.GenericWrite, 
                                            NativeMethods.EFileShare.Read | NativeMethods.EFileShare.Write,
                                            IntPtr.Zero,
                                            NativeMethods.ECreationDisposition.OpenExisting,
                                            NativeMethods.EFileAttributes.BackupSemantics, //0,
                                            IntPtr.Zero);
            if (handle.IsInvalid)
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());

            bool result = false;
            IntPtr pCreationTime = IntPtr.Zero;
            IntPtr pAccessTime = IntPtr.Zero;
            IntPtr pWriteTime = IntPtr.Zero;
            try
            {
                if (ftCreationTime.HasValue)
                {
                    pCreationTime = Marshal.AllocHGlobal(sizeof(long));
                    Marshal.WriteInt64(pCreationTime, 0, ftCreationTime.Value.ToFileTime());
                }

                if (ftLastAccessTime.HasValue)
                {
                    pAccessTime = Marshal.AllocHGlobal(sizeof(long));
                    Marshal.WriteInt64(pAccessTime, 0, ftLastAccessTime.Value.ToFileTime());
                }

                if (ftLastWriteTime.HasValue)
                {
                    pWriteTime = Marshal.AllocHGlobal(sizeof(long));
                    Marshal.WriteInt64(pWriteTime, 0, ftLastWriteTime.Value.ToFileTime());
                }

                result = SetFileTime(handle, pCreationTime, pAccessTime, pWriteTime);
            }
            finally
            {
                if (pCreationTime != IntPtr.Zero)
                    Marshal.FreeHGlobal(pCreationTime);

                if (pAccessTime != IntPtr.Zero)
                    Marshal.FreeHGlobal(pAccessTime);

                if (pWriteTime != IntPtr.Zero)
                    Marshal.FreeHGlobal(pWriteTime);
            }

            handle.Close();

            if (!result)
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());

        } //--HSetDirFileTime

        public static String HGetShortName(String dirFileName)
        {
            String longName = HSortPathFileName(dirFileName);
            if (longName == "")
                return ""; // throw new System.Exception("Directory path or file name to get short name for is empty");

            uint result = GetShortPathName(longName, null, 0);
            if (result == 0)
                return "";

            StringBuilder shortNameBuffer = new StringBuilder((int)result);
            result = GetShortPathName(longName, shortNameBuffer, result);
            return shortNameBuffer.ToString();
        } //--HGetShortPathName

        public static UInt64 HGetFileSize(String fileName)
        {
            String longName = HSortPathFileName(fileName);
            if (longName == "")
                throw new System.Exception("File name to get size for is empty");

            NativeMethods.Win32FindDataStruct w32FindDtStruct = new NativeMethods.Win32FindDataStruct();
            IntPtr findHandle = NativeMethods.FindFirstFile(longName, out w32FindDtStruct);
            if (findHandle == (IntPtr)(-1) /*INVALID_HANDLE_VALUE*/)
                throw new System.Exception("Can't get handle for FindFirstFile(), probably due to access denied with file \"" + fileName + "\".");
            NativeMethods.FindClose(findHandle);

            return (((UInt64) w32FindDtStruct.nFileSizeHigh) << 32) | w32FindDtStruct.nFileSizeLow;
        }
        #endregion
    }
}

