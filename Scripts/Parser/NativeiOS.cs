//*************************************************************************
//	创建日期:	2017-10-11
//	文件名称:	NativeiOS.cs
//  创建作者:	Rect 	
//	版权所有:	shadowkong.com
//	相关说明:	
//*************************************************************************

//-------------------------------------------------------------------------
using System.Runtime.InteropServices;

namespace sk
{
    class NativeiOS
    {
        //-------------------------------------------------------------------------
        public const char m_bSplit = '-';
        public const string m_strSplit = "-00";
        //-------------------------------------------------------------------------
        [DllImport("__Internal")]
        public static extern string MFWGB2312toUTF8(string strValue, int nByteCount);
        //-------------------------------------------------------------------------
        [DllImport("__Internal")]
        public static extern string MFWUTF8ToGB2312(string strValue);
        //-------------------------------------------------------------------------
    }
}
