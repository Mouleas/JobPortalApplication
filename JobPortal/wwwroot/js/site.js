﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getConfirmation(id) {
    if (confirm("Are you sure of deleting this data?")) {
        location.replace("DeleteCompany?id="+id);
    }
}

function getDeleteConfirmation(id) {
    if (confirm("Are you sure of deleting this data?")) {
        location.replace("DeleteJob?id=" + id);
    }
}