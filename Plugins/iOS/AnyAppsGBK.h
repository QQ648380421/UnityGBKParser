//
//  AnyAppsGBK.h
//  shadowkong.com
//
//  Created by Rect on 17/10/17.
//
//

#ifndef AnyAppsGBK_h
#define AnyAppsGBK_h

//-------------------------------------------------------------------------
extern "C" const char* MFWGB2312toUTF8(char* pstr,int nLen)
{
    NSString *pStr = [[NSString alloc] initWithFormat:@"%s" ,pstr];
    
    if (0 >= nLen)
    {
        char* res = (char*)malloc(strlen(pstr)+1);
        strcpy(res, pstr);//必须copy一份 避免il2cpp中自动释放内存错误
        return res;
    }
    
    NSStringEncoding encodingGBK = CFStringConvertEncodingToNSStringEncoding(kCFStringEncodingGB_18030_2000);
    NSString* hexString = [pStr stringByReplacingOccurrencesOfString:@"-" withString:@""];
    int j = 0;
    Byte bytes[2 * nLen];
    for(int i=0;i<[hexString length];i++)
    {
        int int_ch;  /// 两位16进制数转化后的10进制数
        
        unichar hex_char1 = [hexString characterAtIndex:i]; ////两位16进制数中的第一位(高位*16)
        int int_ch1;
        if(hex_char1 >= '0' && hex_char1 <='9')
            int_ch1 = (hex_char1-48)*16;   //// 0 的Ascll - 48
        else if(hex_char1 >= 'A' && hex_char1 <='F')
            int_ch1 = (hex_char1-55)*16; //// A 的Ascll - 65
        else
            int_ch1 = (hex_char1-87)*16; //// a 的Ascll - 97
        i++;
        
        unichar hex_char2 = [hexString characterAtIndex:i]; ///两位16进制数中的第二位(低位)
        int int_ch2;
        if(hex_char2 >= '0' && hex_char2 <='9')
            int_ch2 = (hex_char2-48); //// 0 的Ascll - 48
        else if(hex_char1 >= 'A' && hex_char1 <='F')
            int_ch2 = hex_char2-55; //// A 的Ascll - 65
        else
            int_ch2 = hex_char2-87; //// a 的Ascll - 97
        
        int_ch = int_ch1+int_ch2;
        ///将转化后的数放入Byte数组里
        bytes[j] = int_ch;  
        j++;
    }
    
    NSData *data = [[NSData alloc] initWithBytes:bytes length:nLen];
    NSString* GBKString = [[NSString alloc] initWithData:data encoding:encodingGBK];
    
    if (nil == GBKString || [GBKString isEqualToString:nil])
    {
        char* res = (char*)malloc(strlen(pstr)+1);
        //必须copy一份 避免il2cpp中自动释放内存错误
        strcpy(res, pstr);
        return res;
    }

    const char* resNoCopy = [GBKString UTF8String];
    char* res = (char*)malloc(strlen(resNoCopy)+1);
    //必须copy一份 避免il2cpp中自动释放内存错误
    strcpy(res, resNoCopy);
    return res;
}
//-------------------------------------------------------------------------
extern "C" const char* MFWUTF8ToGB2312(char* pstr)
{
    NSString *utf8string = [[NSString alloc] initWithFormat:@"%s" ,pstr];
    NSStringEncoding encodingGBK =CFStringConvertEncodingToNSStringEncoding(kCFStringEncodingGB_18030_2000);
    NSStringEncoding encodingUTF8 = CFStringConvertEncodingToNSStringEncoding(kCFStringEncodingUTF8);
    NSData* gb2312data =  [NSData dataWithBytes:pstr length:strlen(pstr)];
    NSString *retStr = [[NSString alloc] initWithData:gb2312data encoding:encodingGBK];
    const char* resNoCopy = [utf8string cStringUsingEncoding:encodingUTF8];
    
    char* res = (char*)malloc(strlen(resNoCopy)+1);
    //必须copy一份 避免il2cpp中自动释放内存错误
    strcpy(res, resNoCopy);
    return res;
}
//-------------------------------------------------------------------------

#endif /* AnyAppsGBK_h */