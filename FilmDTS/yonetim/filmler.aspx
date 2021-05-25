<%@ Page Title="" Language="C#" MasterPageFile="~/yonetim/yonetim.Master" AutoEventWireup="true" CodeBehind="filmler.aspx.cs" Inherits="FilmDTS.yonetim.fimler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="yoneticiHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="yoneticiBody" runat="server">
    <div class="col-9" style="height: 100%; min-height: 1000px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Filmler</h1>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">
            
            <asp:GridView ID="Filmler1" runat="server"
                AutoGenerateColumns="False" OnRowCancelingEdit="Filmler1_RowCancelingEdit" OnRowDeleting="Filmler1_RowDeleting" OnRowEditing="Filmler1_RowEditing" OnRowUpdating="Filmler1_RowUpdating"
                DataKeyNames="FilmID">
                <Columns>
                    <asp:BoundField DataField="FilmID" HeaderText="Film ID"/>
                    <asp:BoundField DataField="Ad" HeaderText="Ad"/>
                    <asp:BoundField DataField="Sure" HeaderText="Sure"/>
                    <asp:BoundField DataField="KategoriID" HeaderText="Kategori ID"/>
                    <asp:BoundField DataField="SenaristID" HeaderText="Senarist ID"/>
                    <asp:BoundField DataField="YonetmenID" HeaderText="Yönetmen ID"/>
                    <asp:BoundField DataField="VizyonaGirisTarihi" HeaderText="Vizyona Giriş Tarihi"/>
                    <asp:BoundField DataField="ResimURL" HeaderText="ResimURL"/>
                    <asp:BoundField DataField="TanitimBilgisi" HeaderText="Tanıtım Bilgisi"/>
                    <asp:CommandField ShowDeleteButton="True"
                        ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>