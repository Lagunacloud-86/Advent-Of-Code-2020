using System;
using System.Collections.Generic;
using System.Text;

namespace DayFour
{
    public static class Passport
    {
        public static Boolean HasField(
            in ReadOnlySpan<Char> passport, 
            in String field)
        {
            Int32 index = 0;
            while(Inner_NextProperty(
                in passport,
                in index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> propertySpan = passport.Slice(sliceIndex, sliceLength);

                Inner_KeyValuePair(
                    in propertySpan,
                    out Int32 nameIndex, out Int32 nameLength,
                    out Int32 valueIndex, out Int32 valueLength);

                if(propertySpan.Slice(nameIndex, nameLength).SequenceEqual(field.ToCharArray()))
                {
                    return true;
                }

            
                index = sliceIndex + sliceLength + 1;
            }
            //Int32 i = 0, j = 0;
            //for(; i < passportData.Length; ++i)
            //{



            //    if(passportData[i] == ':' || passportData[i] == '\n')
            //    {
            //        for (j = i; j > 0 || passportData[j] == ' ' || passportData[j] == '\n'; j--) { }
            //        ReadOnlySpan<Char> property = passportData.Slice(j, i - j).Trim();
            //        String propertyName = property.ToString();
            //        if (propertyName == field)
            //            return true;
            //    }
            //}

            return false;
        }
        public static void GetFieldValue(
            in ReadOnlySpan<Char> passport,
            in String field,
            out String value)
        {
            Int32 index = 0;
            while (Inner_NextProperty(
                in passport,
                in index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> propertySpan = passport.Slice(sliceIndex, sliceLength);

                Inner_KeyValuePair(
                    in propertySpan,
                    out Int32 nameIndex, out Int32 nameLength,
                    out Int32 valueIndex, out Int32 valueLength);

                if (propertySpan.Slice(nameIndex, nameLength).SequenceEqual(field.ToCharArray()))
                {
                    value = propertySpan.Slice(valueIndex, valueLength).ToString();
                    return;
                }


                index = sliceIndex + sliceLength + 1;
            }
            //Int32 i = 0, j = 0;
            //for(; i < passportData.Length; ++i)
            //{



            //    if(passportData[i] == ':' || passportData[i] == '\n')
            //    {
            //        for (j = i; j > 0 || passportData[j] == ' ' || passportData[j] == '\n'; j--) { }
            //        ReadOnlySpan<Char> property = passportData.Slice(j, i - j).Trim();
            //        String propertyName = property.ToString();
            //        if (propertyName == field)
            //            return true;
            //    }
            //}

            value = null;
        }


        private static Boolean Inner_NextProperty(
                in ReadOnlySpan<Char> passport,
                in Int32 index,
                out Int32 sliceIndex, out Int32 sliceLength)
        {
            sliceIndex = index;
            sliceLength = 0;
           

            if (index >= passport.Length)
                return false;

            Int32 i = index;
            for (; i < passport.Length; ++i)
            {
                if (passport[i] == ' ' || passport[i] == '\n')
                    break;
            }

            sliceLength = i - sliceIndex;


            return true;

        }
        private static void Inner_KeyValuePair(
                in ReadOnlySpan<Char> propertySlice,
                out Int32 nameIndex, out Int32 nameLength,
                out Int32 valueIndex, out Int32 valueLength)
        {

            Int32 i = 0;
            for (; i < propertySlice.Length; ++i)
            {
                if (propertySlice[i] == ':')
                    break;
            }
            nameIndex = 0;
            nameLength = i;
            valueIndex = i + 1;
            valueLength = propertySlice.Length - (i + 1);
        }






    }
}
