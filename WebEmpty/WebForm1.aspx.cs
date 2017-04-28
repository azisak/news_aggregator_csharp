using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebEmpty
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Parser p;
        protected void Page_Load(object sender, EventArgs e)
        {
            string rss_tempo_link = "http://tempo.co/rss/terkini";
            string rss_viva_link = "http://rss.viva.co.id/get/all";
            p = new Parser(rss_viva_link);
        }

        private int GetIdxByAlgo(int algo_id,string text,string pattern)
        {
            if (algo_id == 0)
            {
                return SearchAlgo.booyerMoore(text.ToLower(), pattern.ToLower());
            } else if(algo_id == 1)
            {
                return SearchAlgo.kmpSearch(text.ToLower(), pattern.ToLower());
            } else //algo_id 2
            {
                return SearchAlgo.regexSearch(text.ToLower(), pattern.ToLower());
            }
        }

        private void cariKeyword(int algo_id,string html_link,string title, string pattern,ref int curr_id)
        {
            string founded;
            string konten = p.parseHTML(html_link);
            int idx_found = GetIdxByAlgo(algo_id,title.ToLower(),pattern.ToLower());
            int start, end;
            if (idx_found >= 0)
            {
                founded = title;
                myDiv.Controls.Add(new Label() { Text = curr_id + ". " });
                myDiv.Controls.Add(new Label() { Text = "<a href=" + html_link + " target='_blank' style='font-size: 14pt'>" + title + "</a>" + "</br>" });
                myDiv.Controls.Add(new Label() { Text = "<font color='green'>" + html_link + "</font>" + "</br>" });
                myDiv.Controls.Add(new Label() { Text = founded + "</br>" });
                
                curr_id++;
            }
            else
            {
                founded = "";
                idx_found = GetIdxByAlgo(algo_id, konten.ToLower(), pattern.ToLower());
                if (idx_found >= 0)
                {
                    //Mencari start position
                    start = idx_found;
                    while (start > 0 && konten[start] != '.')
                    {
                        start--;
                    }
                    if (konten[start] == '.')
                    {
                        start++;
                    }
                    //Mencari end position
                    end = idx_found;
                    while (end < (konten.Length - 1) && konten[end] != '.')
                    {
                        end++;
                    }
                    //generate kalimat
                    for (int id = start; id <= end; id++)
                    {
                        founded = founded + konten[id];
                    }
                    //Format output
                    myDiv.Controls.Add(new Label() { Text = curr_id + ". " });
                    myDiv.Controls.Add(new Label() { Text = "<a href=" + html_link + " target='_blank' style='font-size: 14pt'>" + title + "</a>" + "</br>" });
                    myDiv.Controls.Add(new Label() { Text = "<font color='green'>" + html_link + "</font>" + "</br>" });
                    myDiv.Controls.Add(new Label() { Text = founded + "</br>" });
                    
                    
                        
                    curr_id++;
                }
            }

            if (idx_found >= 0)
            {
                string[] words = title.Split(null);
                foreach (var word in words)
                {
                    if (Kamus.isOnKamus(word.ToLower()))
                    {
                        string alamat = "https://www.google.com/maps?q=" + word + "&output=embed";
                        myDiv.Controls.Add(new Label() { Text = "<iframe src=" + alamat + " width='200' height='150' frameborder='0' style='border:0' allowfullscreen></iframe>" + "</br>" });

                        break;
                    }
                }
                myDiv.Controls.Add(new Label() { Text = "</br>" });
            }
        }
    
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Tuple<string, string>> tuples = p.parseXML();// Item 1 = judul berita, item 2 link berita
            int i = 1;
            int idx_found, start, end;
            string konten;
            string founded = "";
            int nb_parsed = 50;
            Kamus.addKamus();
            if (RadioButtonList1.SelectedIndex == 0 )
            {
                foreach (var tuple in tuples)
                {
                    cariKeyword(RadioButtonList1.SelectedIndex,tuple.Item2, tuple.Item1, TextBox1.Text.ToLower(),ref i);
                    nb_parsed--;
                    if (nb_parsed <= 0)
                    {
                        break;
                    }
                }

            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                foreach(var tuple in tuples)
                {
                    cariKeyword(RadioButtonList1.SelectedIndex, tuple.Item2, tuple.Item1, TextBox1.Text.ToLower(), ref i);
                    nb_parsed--;
                    if (nb_parsed <= 0)
                    {
                        break;
                    }
                }
            }
            else if (RadioButtonList1.SelectedIndex == 2)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Algoritma Regular Expression belum diimplementasikan!');</script>");
                foreach (var tuple in tuples)
                {
                    cariKeyword(RadioButtonList1.SelectedIndex, tuple.Item2, tuple.Item1, TextBox1.Text.ToLower(), ref i);
                    nb_parsed--;
                    if (nb_parsed <= 0)
                    {
                        break;
                    }
                }
            }
           else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Harap pilih algoritma !');</script>");
                /*ArrayList list = Kamus.getIsiKamus();
                foreach (var li in list)
                {
                    myDiv.Controls.Add(new Label() { Text = li + "</br>" });
                }*/
            }
            
        }
        
    }
}