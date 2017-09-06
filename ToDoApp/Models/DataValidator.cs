using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;


//String Validation
//		1- Is a string
//		2- Spell Check and suggest
//		3- Contains words
//		-- Special Characters   !@#$%^&*()-_=+~`{[}]|\;:'",<.>/?/*-+ ±πΣ∫½° ☺
//		4- Only numbers     0123456789
//		5- No Letters       ▐»αßΓ
//		6- Not visible characters  /r/l/t/n EOF
//		7- SQL Injection
//		8- JSON Injection {:,}
//		9- 
//		10- Regular Expression Injection
//		11- Bad Language

//Date Vaildation
//    Due Date after Create Date

namespace ToDoApp.Models
{
    class DataValidator
    {

        static public bool StringIsText(string textString)
        {
            bool noe = string.IsNullOrEmpty(textString);

            return (textString != null && textString != "") ? true : false;
        }

        static public bool TextIsAlphaNum(string textString)
        {
            bool result = false;
            if (textString.All(char.IsLetterOrDigit))
            {
                result = true;
            }
            return result;
        }

        static public bool TextIsAlphaNumReg(string textString)
        {
            Regex regEx = new Regex("^[a-zA-Z0-9]*$");
            bool result = false;

            if (regEx.IsMatch(textString))
            {
                result = true;
            }
            return result;
        }

        // \W matches any non-word character.
        static public bool TextIsWords(string textString)
        {
            Regex regEx = new Regex("[/w/d/s]");
            bool result = false;

            if (regEx.IsMatch(textString))
            {
                result = true;
            }
            return result; 
        }

        static public bool TextIsSentences(string textString)
        {
            Regex regEx = new Regex(@"[a-zA-Z0-9.,!?;:/-]+?");
            bool result = false;

            if (regEx.IsMatch(textString))
            {
                result = true;
            }

            return result;
        }

        static public bool TextIsParagraphs(string textString)
        {
            Regex regEx = new Regex(@"[\w\d\s\r\n\p{Punctuation}\p{Letter}\p{Mark}\p{Currency_Symbol}]+?");
            bool result = false;

            if (regEx.IsMatch(textString))
            {
                result = true;
            }

            return result;
        }

        // \p{Math_Symbol}: any mathematical symbol.   U+2200–U+22FF
        // \p{Currency_Symbol}: any currency sign.
        // \p{Modifier_Symbol}: a combining character (mark) as a full character on its own.
        // \p{InGreek_and_Coptic}: U+0370–U+03FF
        // string pattern = @"\b(\p{IsGreek}+(\s)?)+\p{Pd}\s(\p{IsBasicLatin}+(\s)?)+";
        static public bool TextIsEquation(string textString)
        {
            Regex regEx = new Regex(@"[A-Za-z0-9\p{IsGreekandCoptic}\w\d\s\r\n\(\)\[\]\{\}\\\/\|\-+*^<>=.,:;#%&]+?") ;
            //^[A-Za-z0-9\p{IsGreekandCoptic} \s\t\r|\/\-+*^<>=.,:;()#%/[/]{}]+$");
            //"[^a-zA-Z0-9-()/\s\p{IsGreekandCoptic}]");
            //  [a-zA-Z0-9.,:()#[]{}|/*-+<>=]
            //  /[\u0370 - \u03FF] + /gm
            //  /[\x{0370} - \x{03FF}]+/u
                
            bool result = false;

            if (regEx.IsMatch(textString))
            {
                result = true;
            }

            return result;
        }

        static public bool TextIsNonLetters(string textString)
        {
            //\p{L} or \p{Letter}: any kind of letter from any language.
            bool result = true;
            if (textString.All(char.IsLetter))
            {
                result = false;
            }
            return result;

        }

        static public bool TextIsNumeric(string textString)
        {
            Regex regEx = new Regex(@"[\d.,\-]+?");  //" ^[0-9]*$");
            bool result = false;
            CultureInfo culture;
            culture = CultureInfo.CurrentCulture;

            if (regEx.IsMatch(textString))
            {
                decimal value = -1;
                if (Decimal.TryParse(textString, NumberStyles.Number, culture, out value))
                    result = true;
            }

            if (textString.All(char.IsNumber))
            {
                result = true;
            }

            return result;
        }

        static public bool TextIsCurrency(string textString)
        {
            Regex regEx = new Regex(@"[\d\p{P}\p{Currency_Symbol}]+?");
            bool result = false;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            if (DataValidator.TextIsNumeric(textString))
            {
                if (regEx.IsMatch(textString))
                {
                    decimal value = -1;
                    if (Decimal.TryParse(textString, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, currentCulture, out value))
                        result = true;
                }
            }

            return result;
        }

        //static public bool TextIsEmail(string textString)
        //{
        //    bool result = false;
        //    //var emailCheck = new EmailAddressAttribute();
        //    //result = emailCheck.IsValid(textString);

        //    return result;
        //}

        static public bool TextIsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public bool TextIsURL(string urlString)
        {
            bool isUri = Uri.IsWellFormedUriString(urlString, UriKind.RelativeOrAbsolute);

            Regex UrlMatch = new Regex(@"(?i)(http(s)?:\/\/)?(\w{2,25}\.)+\w{3}([a-z0-9\-?=$-_.+!*()]+)(?i)", RegexOptions.Singleline);
            Regex UrlMatchOnlyHttps = new Regex(@"(?i)(http(s)?:\/\/)(\w{2,25}\.)+\w{3}([a-z0-9\-?=$-_.+!*()]+)(?i)", RegexOptions.Singleline);

            bool resultStd = UrlMatch.IsMatch(urlString);
            bool resultSecure = UrlMatchOnlyHttps.IsMatch(urlString);

            return isUri;
        }


        static public bool TextHasWhitespace(string textString)
        {
            //bool nws = String.IsNullOrWhiteSpace(textString);
            //myString.Any(x => Char.IsWhiteSpace(x))

            bool result = textString.Any(Char.IsWhiteSpace);
            return result;
        }

        static public bool TextHasSpecialCharaters(string textString)
        {
            throw new NotImplementedException();

            bool result = false;
            if (textString.Any(DataValidator.CharIsSpecialCharater))
            {
                result = true;
            }
            return result;

        }

        static public bool CharIsSpecialCharater(char character)
        {
            bool result = false;
            if ((character < '0') || (character > 'z' && character <= 0xFFFF) 
                || (character > '9' && character < 'A') || (character > 'Z' && character < 'a')
                || !(DataValidator.CharIsPrintable(character)) )
            {
                result = true;
            }
            return result;
        }

        // \p{Symbol}: math symbols, currency signs, dingbats, box-drawing characters, etc.
        static public bool TextHasSymbols(string textString)
        {
            bool result = false;
            if (textString.Any(char.IsSymbol))
            {
                result = true;
            }
            return result;

        }

        // \p{Punctuation}: any kind of punctuation character.
        static public bool TextHasPunctuation(string textString)
        {
            bool result = false;
            if (textString.Any(char.IsPunctuation))
            {
                result = true;
            }
            return result;
        }


        static public bool TextHasNonPrintCharaters(string textString)
        {
            bool control = (textString.Any(Char.IsControl));

            bool result = false;
            if (!(textString.Any(DataValidator.CharIsPrintable)) )
            {
                result = true;
            }
            return result;

        }

        static public bool CharIsPrintable(char character)
        {
            // The set of Unicode character categories containing non-rendering,
            // unknown, or incomplete characters.
            // !! Unicode.Format and Unicode.PrivateUse can NOT be included in this set, 
            var nonRenderingCategories = new UnicodeCategory[] {
                UnicodeCategory.Control,
                UnicodeCategory.OtherNotAssigned,
                UnicodeCategory.Surrogate };

            // Char.IsWhiteSpace() includes the ASCII whitespace characters that
            // are categorized as control characters. Any other character is
            // printable, unless it falls into the non-rendering categories.
            bool isPrintable = Char.IsWhiteSpace(character) || !nonRenderingCategories.Contains(Char.GetUnicodeCategory(character));

            return isPrintable;
        }

        static public bool TextHasRistrictedWords(string textString)  //illicit prohibited
        {
            List<string> prohibited = new List<string> { "shit", "fuck", "cunt", "asshole", "cocksucker", "nigger", "ballsack", "biatch",
                "blowjob",  "blow job", "bollock", "bollok", "boner", "buttplug", "dyke", "fag", "feck", "fudgepacker", "fudge packer", "Goddamn",
                "God damn", "nigga", "pussy", "tosser", "twat", "wank" };

            bool result = false;

            foreach (var illicit in prohibited)
            {
                if (textString.Contains(illicit))
                     result = true;
            }

            return result;
        }


        static public bool TextHasFormatSQL(string textString)
        {
            throw new NotImplementedException();

            bool result = false;
           
            return result;

        }


        static public bool TextHasFormatJSON(string textString)
        {
            throw new NotImplementedException();

            bool result = false;
          
            return result;

        }

        static public bool TextHasFormatRegEx(string textString)
        {
            throw new NotImplementedException();

            bool result = false;
            
            return result;

        }


        static public bool DateTimeIsValid(DateTime aDateTime)
        {
            bool result = false;
            if (aDateTime != null && aDateTime != DateTime.MinValue)
            {
                result = true;
            }

            return result;
        }


        static public bool TextIsValidDateTime(string dateTimeString)
        {
            Regex regEx = new Regex(@"[\d\/\- :apm]+?");
            bool result = false;

            if (regEx.IsMatch(dateTimeString))
            {
                result = true;
            }

            return result;

        }

        static public bool TextIsValidDate(string dateString)
        {
            Regex regEx = new Regex(@"[\d\/\-]+?");
            bool result = false;

            if (regEx.IsMatch(dateString))
            {
                result = true;
            }

            return result;
        }

        static public bool TextIsValidTime(string timeString)
        {
            Regex regEx = new Regex(@"[\d: apm]+?");
            bool result = false;

            if (regEx.IsMatch(timeString))
            {
                result = true;
            }

            return result;
        }
    }
}
