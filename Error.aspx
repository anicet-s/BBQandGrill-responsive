<%@ Page Title="Error" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BBQandGrill.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Error - Paul A's Barbecue</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-8 offset-md-2 text-center">
                <h1 class="display-4">Oops! Something went wrong</h1>
                <p class="lead mt-4">We're sorry, but something unexpected happened. Our team has been notified and we're working to fix the issue.</p>
                <div class="mt-5">
                    <a href="Default.aspx" class="btn btn-primary btn-lg">Return to Home</a>
                    <a href="ContactUs.aspx" class="btn btn-outline-secondary btn-lg ms-2">Contact Us</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
