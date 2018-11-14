/// <reference path="../ckfinder/core/connector/aspx/connector.aspx" />
/// <reference path="../ckfinder/ckfinder.html" />
/// <reference path="../ckfinder/ckfinder.html" />
/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For the complete reference:
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Assets/Admin/js/plugin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/Admin/js/plugin/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Assets/Admin/js/plugin/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Assets/Admin/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/Assets/Admin/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Assets/Admin/js/plugin/ckfinder/');
};
