using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions; 
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["KullaniciTipi"] != null &&
                HttpContext.Current.Session["KullaniciID"] != null)
            {
                dynamicNavbar.Visible = true;
                int kullaniciTipi = (int)HttpContext.Current.Session["KullaniciTipi"];
                int kullaniciId = (int)HttpContext.Current.Session["KullaniciID"];

                string kullaniciAdSoyad = Session["KullaniciAdSoyad"]?.ToString() ?? "Kullanıcı";
                string profilFotoUrl = Session["KullaniciFoto"]?.ToString();
                string profilLink = ResolveUrl((kullaniciTipi == 0) ? "~/OgrenciProfil.aspx" : "~/OgretmenProfil.aspx");

                if (string.IsNullOrEmpty(profilFotoUrl))
                {
                    try
                    {
                        if (kullaniciTipi == 0)
                        {
                            List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(kullaniciId);
                            if (ogrList?.Count > 0 && !string.IsNullOrEmpty(ogrList[0].FOTOGRAF))
                            {
                                profilFotoUrl = ResolveUrl($"~/OgrenciFotograflari/{ogrList[0].FOTOGRAF}");
                                Session["KullaniciFoto"] = profilFotoUrl;
                            }
                            else { profilFotoUrl = ResolveUrl("~/OgrenciFotograflari/default.png"); }
                        }
                        else
                        {
                            List<EntityOgretmen> ogrtList = BLLOgretmen.BllDetay(kullaniciId);
                            if (ogrtList?.Count > 0 && !string.IsNullOrEmpty(ogrtList[0].OGRTFOTOGRAF))
                            {
                                profilFotoUrl = ResolveUrl($"~/OgretmenFotograflari/{ogrtList[0].OGRTFOTOGRAF}");
                                Session["KullaniciFoto"] = profilFotoUrl;
                            }
                            else { profilFotoUrl = ResolveUrl("~/OgretmenFotograflari/default_teacher.png"); }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("MasterPage Fotoğraf Çekme Hatası: " + ex.Message);
                        profilFotoUrl = ResolveUrl((kullaniciTipi == 0) ? "~/OgrenciFotograflari/default.png" : "~/OgretmenFotograflari/default_teacher.png");
                    }
                }

                dynamicNavbar.Controls.Clear();
                StringBuilder navbarHtml = new StringBuilder();
                navbarHtml.Append("<div class='container-fluid'>");
                navbarHtml.Append("<div class='navbar-header'>");
                navbarHtml.Append("<button type='button' class='navbar-toggle collapsed' data-toggle='collapse' data-target='#navbarCollapseContent' aria-expanded='false'><span class='sr-only'>...</span><span class='icon-bar'></span><span class='icon-bar'></span><span class='icon-bar'></span></button>");
                string brandText = (kullaniciTipi == 0) ? "OGR" : "OGRT";
                string brandLink = ResolveUrl((kullaniciTipi == 0) ? "~/OgrenciAnaSayfa.aspx" : "~/OgretmenAnaSayfa.aspx");
                navbarHtml.Append($"<a class='navbar-brand' href='{brandLink}'>{brandText}</a>");
                navbarHtml.Append("</div>");

                navbarHtml.Append("<div class='collapse navbar-collapse' id='navbarCollapseContent'>");
                navbarHtml.Append("<ul class='nav navbar-nav'>");
                string currentPage = System.IO.Path.GetFileName(Request.PhysicalPath);
                if (kullaniciTipi == 0)
                {
                    AddNavLink(navbarHtml, ResolveUrl("~/OgrenciAnaSayfa.aspx"), "<span class='glyphicon glyphicon-home'></span> Ana Sayfa", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/DersTalep.aspx"), "<span class='glyphicon glyphicon-edit'></span> Ders Talep", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/OgrenciDerslerim.aspx"), "<span class='glyphicon glyphicon-list-alt'></span> Derslerim", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/BakiyeYukle.aspx"), "<span class='glyphicon glyphicon-credit-card'></span> Bakiye Yükle", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/OgrenciDuyurular.aspx"), "<span class='glyphicon glyphicon-bullhorn'></span> Duyurular", currentPage);
                }
                else
                {
                    AddNavLink(navbarHtml, ResolveUrl("~/OgretmenAnaSayfa.aspx"), "<span class='glyphicon glyphicon-home'></span> Ana Sayfa", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/DersListesi.aspx"), "<span class='glyphicon glyphicon-th-list'></span> Ders Yönetimi", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/OgretmenDerslerim.aspx"), "<span class='glyphicon glyphicon-check'></span> Derslerim", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/OgrenciListesi.aspx"), "<span class='glyphicon glyphicon-user'></span> Öğrenci Yönetimi", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/BasvuruListesi.aspx"), "<span class='glyphicon glyphicon-inbox'></span> Başvurular", currentPage);
                    AddNavLink(navbarHtml, ResolveUrl("~/DuyuruYonetimi.aspx"), "<span class='glyphicon glyphicon-bullhorn'></span> Duyuru Yönetimi", currentPage);
                }
                navbarHtml.Append("</ul>");


                navbarHtml.Append("<ul class='nav navbar-nav navbar-right'>");

                string mesajUrl = ResolveUrl((kullaniciTipi == 0) ? "~/OgrenciMesajlar.aspx" : "~/OgretmenMesajlasma.aspx");
                navbarHtml.Append($"<li><a href='{mesajUrl}' title='Mesajlar' class='navbar-icon-link'><span class='glyphicon glyphicon-envelope' style='font-size:1.1em;'></span><span class='sr-only'>Mesajlar</span></a></li>");

                navbarHtml.Append("<li class='dropdown'>");
                navbarHtml.Append($"<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'>");
                navbarHtml.Append($"<img src='{profilFotoUrl}' alt='Profil' class='navbar-profile-pic'> "); // Resim
                navbarHtml.Append($"{kullaniciAdSoyad} <span class='caret'></span>"); // İsim ve ok
                navbarHtml.Append("</a>");
                navbarHtml.Append("<ul class='dropdown-menu'>");
                navbarHtml.Append($"<li><a href='{profilLink}'><span class='glyphicon glyphicon-user'></span> Profilim</a></li>");
                navbarHtml.Append("<li role='separator' class='divider'></li>");
                navbarHtml.Append($"<li><a href='{ResolveUrl("~/Logout.aspx")}'><span class='glyphicon glyphicon-log-out'></span> Çıkış Yap</a></li>");
                navbarHtml.Append("</ul>");
                navbarHtml.Append("</li>");
                navbarHtml.Append("</ul>");

                navbarHtml.Append("</div>");
                navbarHtml.Append("</div>");

                dynamicNavbar.Controls.Add(new LiteralControl(navbarHtml.ToString()));
            }
            else
            {
                dynamicNavbar.Visible = false;
            }
        }

        private void AddNavLink(StringBuilder sb, string url, string text, string currentPageFilename, bool checkActive = true)
        {
            string activeClass = "";
            try
            {
                // Aktif linki belirleme mantığı (Mevcut kodunuzdaki gibi kalmalı)
                Uri resolvedUri = new Uri(Page.ResolveUrl(url), UriKind.RelativeOrAbsolute);
                string targetFilename = System.IO.Path.GetFileName(resolvedUri.IsAbsoluteUri ? resolvedUri.AbsolutePath : url.Split('?')[0]);

                if (checkActive && currentPageFilename.Equals(targetFilename, StringComparison.OrdinalIgnoreCase))
                {
                    activeClass = " class='active'";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("AddNavLink URL Hatası: " + ex.Message + " URL: " + url);
                // Fallback aktif link kontrolü (Mevcut kodunuzdaki gibi kalmalı)
                if (checkActive && !string.IsNullOrEmpty(url) && Request.Url.AbsolutePath.EndsWith(url.Substring(url.LastIndexOf('/')), StringComparison.OrdinalIgnoreCase))
                {
                    activeClass = " class='active'";
                }
            }

            // Gelen 'text' içinden ikonu ve asıl metni ayıralım
            string iconHtml = "";
            string linkTextOnly = text; // Varsayılan

            var match = Regex.Match(text, @"(<span\s+class=.*glyphicon.*?></span>\s*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (match.Success)
            {
                iconHtml = match.Groups[1].Value;
                linkTextOnly = Regex.Replace(text, @"(<span\s+class=.*glyphicon.*?></span>\s*)", "").Trim(); // İkonu metinden çıkar
            }

            // li etiketini başlat
            sb.Append($"<li{activeClass}>");
            // a etiketini başlat
            sb.Append($"<a href='{ResolveUrl(url)}'>");
            // İkonu ekle (varsa)
            sb.Append(iconHtml);
            // Metni kendi span'ı içinde ekle
            sb.Append($"<span class='link-text'>{linkTextOnly}</span>");
            // Hover efekt noktası için span ekle
            sb.Append("<span class='hover-dot'></span>");
            // a etiketini kapat
            sb.Append("</a>");
            // li etiketini kapat
            sb.Append("</li>");
        }
    }
}