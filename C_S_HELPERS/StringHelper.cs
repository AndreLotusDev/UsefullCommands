using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class StringHelper
    {
        public static string ToReverseString(this string value)
        {
            return new string(value.Reverse().ToArray());
        }

        /// <summary>
        /// Used with to reverse and substring reverse only
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int LastIndexOfReverse(this string value, string toFound)
        {
            return string.Concat(value.Reverse()).LastIndexOf(toFound);
        }

        public static string Remove(this string value, string partToRemove)
        {
            var stringWithRemovePart = value.Replace(partToRemove, string.Empty);
            return stringWithRemovePart;
        }

        public static string SubstringReverse(this string value, int index)
        {
            return value.ToReverseString().Substring(index).ToReverseString();
        }

        public static string FirstCharToUpper(this string input)
        {
            if(input.Length == 0)
            {
                return input;
            }

            if (!input.Contains(" "))
            {
                var firstLetter = input[0].ToString().ToUpper();
                var allOtherLetter = input.Substring(1).ToLower();

                return firstLetter + allOtherLetter;
            }

            var words = input.Split(" ").ToList();
            if(words.Last() == " " || words.Last() == "")
            {
                words.RemoveAt(words.IndexOf(words.Last()));
            }

            var wordsFormatted = words.Select(w => w[0].ToString().ToUpper() + w.Substring(1).ToLower() + " ").ToList();
            var toReturn = String.Concat(wordsFormatted);

            return toReturn;
        }

        public static string TranslateEnum(EReturnMessageOperation eReturnMessageOperation)
        {
            switch (eReturnMessageOperation) {
                case EReturnMessageOperation.SUCCESSFULLY_ADDED:
                    return "Successfully added!";
                case EReturnMessageOperation.SUCCESSFULLY_UPDATED:
                    return "Successfully updated!";
                case EReturnMessageOperation.SUCCESSFULLY_DELETED:
                    return "Successfully deleted!";
                case EReturnMessageOperation.SUCCESSFULLY_EDITED:
                    return "Successfully edited!";
                case EReturnMessageOperation.SUCCESSFULLY_UPLOADED:
                    return "Successfully uploaded!";
                case EReturnMessageOperation.SOMETHING_WENT_WRONG:
                    return "Something went wrong!";
                case EReturnMessageOperation.FOUND:
                    return "Found!";
                case EReturnMessageOperation.NOT_FOUND:
                    return "Not Found!";
                default:
                    return String.Empty;
            }
        }

        public static string JointAllMessages(IEnumerable<string> messages)
        {
            return String.Join("",messages.Select(s => "| " + s + " |"));
        }

    }

    public enum EReturnMessageOperation
    {
        SUCCESSFULLY_ADDED,
        SUCCESSFULLY_UPDATED,
        SUCCESSFULLY_DELETED,
        SUCCESSFULLY_EDITED,
        SUCCESSFULLY_UPLOADED,
        SOMETHING_WENT_WRONG,
        FOUND,
        NOT_FOUND
    }
}
