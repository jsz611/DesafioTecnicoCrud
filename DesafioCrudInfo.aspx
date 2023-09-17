<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesafioCrudInfo.aspx.cs" Inherits="DesafioTecnicoCrud.DesafioCrudInfo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Desafio Bootcamp</title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style11 {
            height: 146px;
        }
        .auto-style12 {
            text-align: center;
            height: 146px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1" colspan="2"><strong>Informações do Produto</strong></td>
                    <td rowspan="9">
                        <div class="auto-style3">
                            <em>
                                <asp:GridView ID="dgViewProducts" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgViewProducts_SelectedIndexChanged" DataKeyNames="ProductID" CssClass="auto-style8" EmptyDataText="Adicione um produto">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False" ReadOnly="True" SortExpression="ProductID" />
                                        <asp:BoundField DataField="NameItem" HeaderText="NameItem" SortExpression="NameItem" />
                                        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                                        <asp:BoundField DataField="DescriptionProduct" HeaderText="DescriptionProduct" SortExpression="DescriptionProduct" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                        <asp:BoundField DataField="RegistrationDate" HeaderText="RegistrationDate" SortExpression="RegistrationDate" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" SortExpression="UpdateDate" />
                                    </Columns>
                                </asp:GridView>
                            </em>
                        </div>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="LbNameItem" runat="server" Text="Nome do Produto"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:TextBox ID="TxtNameItem" runat="server" Width="311px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="LbPrice" runat="server" Text="Preço"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:TextBox ID="TxtPrice" runat="server" Width="304px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="LblDescriptionProduct" runat="server" Text="Descrição do Produto"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:TextBox ID="TxtDescriptionProduct" runat="server" Width="307px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="LblQuantity" runat="server" Text="Quantidade"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:TextBox ID="TxtQuantity" runat="server" Width="307px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="LblSID" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="LblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style9">
                        <div class="btn-group">
                            <asp:Button ID="BtnAdd" runat="server" Text="Adicionar" CssClass="btn" OnClick="BtnAdd_Click" Width="91px" />
                            <asp:Button ID="BtnUpdate" runat="server" Text="Atualizar" CssClass="btn" OnClick="BtnUpdate_Click" Width="90px" />
                            <asp:Button ID="BtnDelete" runat="server" Text="Excluir" CssClass="btn btn-cancel" OnClick="BtnDelete_Click" Width="90px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style10">
                        <asp:Button ID="BtnCancelOperation" runat="server" Text="Cancelar Operação" CssClass="btn btn-cancel" OnClick="BtnCancelOperation_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
