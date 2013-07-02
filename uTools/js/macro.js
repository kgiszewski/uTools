$(function(){
    $(".editMacro").live('click', function(event){
    
        var $link=$(this);

        UmbClientMgr.openModalWindow(
          '/umbraco/developer/macros/editMacro.aspx?macroID='+$link.attr('rel'), 
          'Edit', 
          true, 
          $(window).width(), 
          $(window).height(),
          0,
          0,
          '',
          function(){}
        );
    });

    $(".editMacroScript").live('click', function(event){
    
        var $link=$(this);

        UmbClientMgr.openModalWindow(
          '/umbraco/developer/python/editPython.aspx?file='+$link.attr('rel'), 
          'Edit', 
          true, 
          $(window).width(), 
          $(window).height(),
          0,
          0,
          '',
          function(){}
        );
    });

    $(".editMacroXslt").live('click', function(event){
    
        var $link=$(this);

        UmbClientMgr.openModalWindow(
          '/umbraco/developer/xslt/editXslt.aspx?file='+$link.attr('rel'), 
          'Edit', 
          true, 
          $(window).width(), 
          $(window).height(),
          0,
          0,
          '',
          function(){}
        );
    });
});