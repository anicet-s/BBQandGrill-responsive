<%@ Page Title="Page Not Found" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="BBQandGrill.NotFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Page Not Found - Paul A's Barbecue</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-8 offset-md-2 text-center">
                <h1 class="display-1">404</h1>
                <h2 class="display-4">Page Not Found</h2>
                <p class="lead mt-4">Sorry, we couldn't find the page you're looking for. It might have been moved or deleted.</p>
                <div class="mt-5">
                    <a href="Default.aspx" class="btn btn-primary btn-lg">Return to Home</a>
                    <a href="Menu.aspx" class="btn btn-outline-secondary btn-lg ms-2">View Menu</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
