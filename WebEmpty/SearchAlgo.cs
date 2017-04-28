using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace WebEmpty
{
    public class SearchAlgo
    {

        public static int booyerMoore(string text, string pattern)
        {
            /* SHIFTING */
            bool matched = false;
            int idx = 0;
            while (!matched && idx + pattern.Length <= text.Length)
            {
                bool sama = true;
                for (int i = pattern.Length - 1; i >= 0 && sama; i--)
                {
                    if (text[idx + i] != pattern[i])
                    {
                        int j = i;
                        int shift = 0;
                        while (j >= 0 && pattern[j] != text[idx + i])
                        {
                            shift++;
                            j--;
                        }
                        idx += shift;
                        sama = false;
                    }
                }
                if (sama)
                {
                    matched = true;
                }
            }

            if (matched)
            {
                return idx;
            }
            else
            {
                return -1;
            }
        }

        public static int regexSearch(string text, string keyword)
        {
            if (!Regex.IsMatch(text, keyword))
            {
                return -1;
            }
            else
            {
                return Regex.Match(text, keyword).Index;
            }
        }

        public static int kmpSearch(string str, string pat)
        {
            List<int> retVal = new List<int>();
            int M = pat.Length;
            int N = str.Length;
            int i = 0;
            int j = 0;
            bool found = false;
            int[] lps = new int[M];

            ComputeArray(pat, M, lps);
            while (i < N)
            {
                if (pat[j] == str[i])
                {
                    j++;
                    i++;
                }

                if (j == M)
                {
                    retVal.Add(i - j);
                    found = true;
                    break;
                }

                else if (i < N && pat[j] != str[i])
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
            }
            if (found)
            {
                return (i - j);
            } else
            {
                return -1;
            }
            
        }

        private static void ComputeArray(string pat, int m, int[] lps)
        {
            int len = 0;
            int i = 1;

            lps[0] = 0;

            while (i < m)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }

    }
}