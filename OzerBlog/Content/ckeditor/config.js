/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'tr';
    // config.uiColor = '#AADC6E';

    //config.extraPlugins = 'codesnippet';
    //config.codeSnippet_theme = 'monokai_sublime';

    config.extraPlugins = 'syntaxhighlight';
    config.entities = false;
    config.entities_latin = false;
    config.entities = false
    config.basicEntities = false;
    config.entities_greek = false;
    config.entities_latin = false;

    //config.extraPlugins = 'lineutils';
};




