<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="kullanici.aspx.cs" Inherits="FilmDTS.arama.kullanici" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">

    <div class="col-9" style="height: 100%; min-height: 1000px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h4>Kullanıcı Arama</h4>
        </div>
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <form class="col-10">
                <div class="form-group row">
                    <label for="inputArananKelime" class="col-sm-4 col-form-label">Kullanıcı arayın</label>
                    <div class="col-sm-6">
                        <asp:textbox runat="server" type="text" class="form-control" id="inputArananKelime" placeholder="kullanıcı adı" required></asp:textbox>
                    </div>
                    <div class="col-sm-2">
                        <asp:button runat="server" type="submit" class="btn btn-primary" id="AraBtn" text="Ara" onclick="AraBtn_Click"></asp:button>
                    </div>
                </div>
            </form>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">

            <asp:gridview id="Kullanicilar1" runat="server"
                autogeneratecolumns="False" datakeynames="KullaniciAdi">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KullaniciAdi"
                        DataNavigateUrlFormatString="/kullanici/{0}"
                        DataTextField="KullaniciAdi" HeaderText="Kullanıcı Adı" />
                </Columns>
            </asp:gridview>
        </div>
    </div>

</asp:Content>
