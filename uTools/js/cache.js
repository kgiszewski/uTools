function loadXML($placeholder, url){
  $.ajax({
    type: "GET",
    async: false,
    url: url,
    contentType: "text/xml; charset=utf-8",
    dataType: "xml",
    success: function (xml){
      $xml=$(xml);
      //console.log($xml);
      renderXML($placeholder, $xml.find('uTools'), 0);
    }
  });
}

$(function(){
  var $resultDiv=$('#resultsWrapper');
  var $searchFrame=$('#searchFrame');
  
  $('form').keypress(function(e){
  
    var code = (e.keyCode ? e.keyCode : e.which);
    if(code == 13) {//press enter
      e.preventDefault();
      getXml($('.xPath').val());
    }
  });
  
  function getXml(xpath){
    if(xpath.length>2){
      loadXML($resultDiv,"/umbraco/plugins/uTools/xpathcache.aspx?xpath="+encodeURI(xpath));
      addToggle();
    }
  }
    
  $('.search').click(function(){
  
    $resultDiv.html('');
    getXml($('.xPath').val());
  });
  
  
  $('.clear').click(function(){
    $resultDiv.html('');
  });
  
  $('.toggle').click(function(){
    $('.toggleNode').click();
  });
});

