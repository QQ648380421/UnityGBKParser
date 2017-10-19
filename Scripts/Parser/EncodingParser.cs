//*************************************************************************
//	创建日期:	2017-10-11
//	文件名称:	EncodingParser.cs
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
    class EncodingParser
    {
        #region Member variables
        //-------------------------------------------------------------------------
        protected Encoding m_GB2312;
        protected Encoding m_UTF8;
        protected Encoding m_ASCII;
        //-------------------------------------------------------------------------
        #endregion

        #region Public Method
        //-------------------------------------------------------------------------
        public EncodingParser()
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
            {
                // Mono编译模式下
                // m_GB2312 = Encoding.GetEncoding(936);

                // iL2CPP编译模式下
                m_GB2312 = new iOSGBKEncoding();

            }
            else
            {
                m_GB2312 = Encoding.GetEncoding("gb2312");

            }
            m_UTF8 = Encoding.UTF8;
            m_ASCII = Encoding.ASCII;
        }
        //-------------------------------------------------------------------------
        public string GB2312ToUtf8(string gb2312String)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Debug.Log("_GB2312ToUtf8 转换前 = " + gb2312String);
                string s = NativeiOS.MFWGB2312toUTF8(gb2312String, gb2312String.Split(NativeiOS.m_bSplit).Length);
                Debug.Log("_GB2312ToUtf8 转换后 = " + s);
                
                return s;
            }
            else
            {
                return _EncodingConvert(gb2312String, m_GB2312, m_UTF8);
            }
        }
        //-------------------------------------------------------------------------
        public string Utf8ToGB2312(string utf8String)
        {

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Debug.Log("_Utf8ToGB2312 转换前 = " + utf8String);
                string s = NativeiOS.MFWUTF8ToGB2312(utf8String);
                Debug.Log("_Utf8ToGB2312 转换后 = " + s);
                return s;
            }
            else
            {
                return _EncodingConvert(utf8String, m_UTF8, m_GB2312);
            }
        }
        //-------------------------------------------------------------------------
        public string ASCIIToUtf8(string asciiString)
        {
            return _EncodingConvert(asciiString, m_ASCII, m_UTF8);
        }
        //-------------------------------------------------------------------------
        public string Utf8ToASCII(string utf8String)
        {
            return _EncodingConvert(utf8String, m_UTF8, m_ASCII);
        }
        //-------------------------------------------------------------------------
        #endregion

        #region private Method
        //-------------------------------------------------------------------------
        private string _EncodingConvert(string fromString, Encoding fromEncoding, Encoding toEncoding)
        {

            byte[] fromBytes = fromEncoding.GetBytes(fromString);
            byte[] toBytes = Encoding.Convert(fromEncoding, toEncoding, fromBytes);

            string toString = toEncoding.GetString(toBytes);
            return toString;


        }
        //-------------------------------------------------------------------------
        #endregion

    }
}
