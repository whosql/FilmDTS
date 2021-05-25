<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FilmDTS._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
    <style>
        .outer {
            display: table;
            position: absolute;
            height: 100%;
            width: 98%;
        }

        .middle {
            display: table-cell;
            vertical-align: middle;
        }

        .inner {
            margin-left: auto;
            margin-right: auto;
            width: 400px;
            /*whatever width you want*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
    <%--<%= Session["KullaniciAdi"] %>--%>
    <div class="col-4" style="margin-bottom: 30px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Kullanıcı Önerileri</h1>
        </div>
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h3>Kullanıcı Öneri 1</h3>
            <asp:GridView ID="K_Oneri1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KullaniciID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KullaniciAdi"
                                        DataNavigateUrlFormatString="/kullanici/{0}"
                                        DataTextField="KullaniciAdi" HeaderText="Kullanıcı Adı"/>
                    <asp:BoundField DataField="OrtakFilmSayisi" HeaderText="Ortak İzlenen Film Sayısı" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h3>Kullanıcı Öneri 2</h3>


        </div>
                                        <div class="row" style="padding: 10px 0px 0px 20px;">
                                                <asp:GridView ID="K_Oneri2" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KullaniciAdi">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KullaniciAdi"
                        DataNavigateUrlFormatString="/kullanici/{0}"
                        DataTextField="KullaniciAdi" HeaderText="Kullanıcı Adı" />
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                        DataNavigateUrlFormatString="/film/{0}"
                        DataTextField="Ad" HeaderText="Film Adı" />
                    
                    <asp:BoundField DataField="Puan" HeaderText="Puan" />
                </Columns>
            </asp:GridView>
                                </div>
    </div>
    <div class="col-6" style="background-color:white; min-height:650px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Film Tavsiye</h1>
        </div>
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h3>Film Öneri 1</h3>     
        </div>
        <div class="row" style="padding: 0px 0px 0px 20px;">
            <asp:GridView ID="F_Oneri1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="FilmID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                        DataNavigateUrlFormatString="/film/{0}"
                        DataTextField="Ad" HeaderText="Film Adı" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h3>Film Öneri 2</h3>     
        </div>


        <div class="row" style="padding: 0px 0px 0px 20px;">

            <asp:GridView ID="F_Oneri2" runat="server"
                AutoGenerateColumns="False" DataKeyNames="FilmID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                        DataNavigateUrlFormatString="/film/{0}"
                        DataTextField="Ad" HeaderText="Film Adı" />
                    <asp:BoundField DataField="IzlenmeSayisi" HeaderText="Kaç Arkadaşım İzledi" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h3>Film Öneri 3</h3>     
        </div>
        <div class="row" style="padding: 0px 0px 0px 20px;">
            <asp:GridView ID="F_Oneri3" runat="server"
                AutoGenerateColumns="False" DataKeyNames="FilmID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                        DataNavigateUrlFormatString="/film/{0}"
                        DataTextField="Ad" HeaderText="Film Adı" />
                </Columns>
            </asp:GridView>
        </div>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
    <div class="outer" style="height:675px; background: url('fotograf/bg-light.jpg'); background-repeat: no-repeat;
    background-size: 100% 100%">
        <div class="middle" style="color:white;">
            <div class="row d-flex justify-content-center">
                <h1 style="color:#4cff00;">Film Bulma Sitesi</h1>
            </div>
            <div class="row d-flex justify-content-center">
                <h3 style="color: teal"></h3>
            </div>
            <div class="row d-flex justify-content-center">
                <h3 style="color:#4cff00;">Hasan AYDOĞAN</h3>
            </div>
            <div class="row d-flex justify-content-center">
                <button type="button" class="btn btn-primary" style="margin-right:20px;" onclick="window.location.href = '/kayit';">Üye Olun</button>
                <button type="button" class="btn btn-primary" onclick="window.location.href = '/giris';">Giriş Yapın</button>
            </div>
         </div>
    </div>
</asp:Content>