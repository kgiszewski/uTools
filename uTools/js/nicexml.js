function addToggle(){
  $('.toggleNode').click(function(){
    var $thisButton=$(this);
    var $nestedDivs=$thisButton.closest('.element').find('.element');
    
    if($thisButton.text()=='-'){
      $nestedDivs.hide();
      $thisButton.text('+');
    } else {
      $nestedDivs.show();
      $thisButton.text('-');
    }
  })
};

function renderXML($placeholder, $xml, level){
  //add the passed in element
  
  //grab reference to just the tag given to us
  var $thisXML=$xml.clone().children().remove().end();
  var attributes=$thisXML[0].attributes;
  
  var attributeString="";
  
  for(var i=0;i<attributes.length;i++){
    attributeString+=' <span class="attributeName">'+attributes[i].nodeName+'</span>="<span class="attributeValue">'+attributes[i].nodeValue+'</span>"';
  }
  
  var tagName=$xml.prop('tagName');
  
  var $elementDiv=$("<div class='element level"+level+"'></div>");
  
  var $startTag;
  var $endTag;
  var $innerDiv;
  
  if($xml.children().length){
    $startTag=$("<div class='openTag'><a class='toggleNode' href='#'>-</a><span class='angle'>&lt;</span>"+tagName+" "+attributeString+"<span class='angle'>&gt;</span></div>");
    $endTag=$("<div class='endTag'><span class='angle'>&lt;</span>/"+tagName+"<span class='angle'>&gt;</span></div>");
  } else {
    $startTag=$("<span class='openTag'><span class='angle'>&lt;</span>"+tagName+" "+attributeString+"<span class='angle'>&gt;</span></span>");
    $endTag=$("<span class='endTag'><span class='angle'>&lt;</span>/"+tagName+"<span class='angle'>&gt;</span></span>");
  }
  
  $elementDiv.append($startTag);
  $elementDiv.append($endTag);
  
  if($xml.children().length){
    $innerDiv=$("<div class='innerElement'></div>");
  } else {
    $innerDiv=$("<span class='innerElement'></span>");
  }
  
  $innerDiv.html("<span class='innerText'>"+$thisXML.text()+"</span>");
  $elementDiv.find('.openTag').append($innerDiv);
  $placeholder.append($elementDiv);
  level++;
  
  //console.log($xml);
  
  $xml.children().each(function(){
    var $thisElement=$(this);
    //console.log($elementDiv);
    renderXML($innerDiv, $thisElement, level);
  });
}