﻿$(document).ready(function () {
    $('#tablaMedicinas').DataTable();
    $('#liHome')[0].firstChild.className = '';
    $('#liUser')[0].firstChild.className = '';
    $('#liPer')[0].firstChild.className = '';
    $('#liMedi')[0].firstChild.className = 'active';
    $('#liPaci')[0].firstChild.className = '';
});