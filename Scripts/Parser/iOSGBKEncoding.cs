//*************************************************************************
//	创建日期:	2017-10-11
//	文件名称:	iOSGBKEncoding.cs
//  创建作者:	Rect 	
//	版权所有:	shadowkong.com
//	相关说明:	
//*************************************************************************

//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace sk
{
    class iOSGBKEncoding : Encoding
    {
        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public override int GetByteCount(char[] chars, int index, int count)
        {
            return UTF8.GetByteCount(chars, index, count);
        }
        //-------------------------------------------------------------------------
        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            return UTF8.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
        }
        //-------------------------------------------------------------------------
        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            return UTF8.GetCharCount(bytes, index, count);
        }
        //-------------------------------------------------------------------------
        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            return UTF8.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
        }
        //-------------------------------------------------------------------------
        public override int GetMaxByteCount(int charCount)
        {
            return UTF8.GetMaxByteCount(charCount);
        }
        //-------------------------------------------------------------------------
        public override int GetMaxCharCount(int byteCount)
        {
            return UTF8.GetMaxCharCount(byteCount);
        }
        //-------------------------------------------------------------------------
        public override string GetString(byte[] bytes, int index, int count)
        {
            if (null == bytes || count > bytes.Length)
            {
                return string.Empty;
            }

            byte[] pp = new byte[count];
            Array.Clear(pp, 0, count);
            Array.Copy(bytes, index, pp, 0, count);
            string strAll = BitConverter.ToString(pp);
            string strHex = strAll.Replace(NativeiOS.m_strSplit, "");
            int nIndex = strAll.IndexOf(NativeiOS.m_strSplit);

            if (-1 != nIndex)
            {
                strHex = strAll.Substring(0, nIndex);
            }
            string strNormal = UTF8.GetString(bytes, index, count).Trim('\0'); 

            StringBuilder sb = new StringBuilder();
            sb.Append("_GB2312ToUtf8 BitConverter = " + BitConverter.ToString(pp));
            sb.Append(" / ");
            sb.Append("_GB2312ToUtf8 strHex = " + strHex);
            sb.Append(" / ");
            sb.Append("_GB2312ToUtf8 strNormal = " + strNormal);
            Debug.Log(sb.ToString());

            return strHex;

        }
        //-------------------------------------------------------------------------
    }
}
