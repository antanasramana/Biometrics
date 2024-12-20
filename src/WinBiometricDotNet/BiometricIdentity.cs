﻿using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using WinBiometricDotNet.Interop;

namespace WinBiometricDotNet
{
    /// <summary>
    /// The <see cref="BiometricIdentity"/> class contains an identifying value associated with a biometric template.
    /// </summary>
    public sealed class BiometricIdentity
    {
        public BiometricIdentity() { }

        #region Constructors

        public unsafe BiometricIdentity(SafeNativeMethods.WINBIO_IDENTITY* identity)
        {
            this.Type = (IdentityType)identity->Type;
            var value = identity->Value;

            switch (this.Type)
            {
                case IdentityType.Null:
                    break;
                case IdentityType.WildCard:
                    break;
                case IdentityType.Guid:
                    unsafe
                    {
                        var templateGuid = value.TemplateGuid;
                        var a = (int)templateGuid.Data1;
                        var b = (short)templateGuid.Data2;
                        var c = (short)templateGuid.Data3;
                        var d = new byte[8];
                        Marshal.Copy((IntPtr)templateGuid.Data4, d, 0, d.Length);
                        this.TemplateGuid = new Guid(a, b, c, d);
                    }
                    break;
                case IdentityType.Sid:
                    unsafe
                    {
                        var accountSid = value.AccountSid;
                        var binaryForm = new byte[accountSid.Size];
                        Marshal.Copy((IntPtr)accountSid.Data, binaryForm, 0, binaryForm.Length);
                        this.Sid = new SecurityIdentifier(binaryForm, 0);
                    }
                    break;
            }

            this.Source = *identity;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the format of the identity information contained in this class.
        /// </summary>
        public IdentityType Type { get; set; }

        /// <summary>
        /// Gets a value that contains an account SID.
        /// </summary>
        public SecurityIdentifier Sid { get; set; }

        internal SafeNativeMethods.WINBIO_IDENTITY Source { get; }

        /// <summary>
        /// Gets a 128-bit GUID value that identifies the template.
        /// </summary>
        public Guid TemplateGuid { get; set; }

        #endregion
    }
}
