define([
    'jquery',
    'exchange'
],function($, exchange){
    'use strict';
    var element = "mainContent";
    var setParametersGet = function (url, targetId) {
        alert(targetId);
        targetId = (targetId=="undefined") ? element : targetId;
        alert(element);
        alert(targetId);
        var parameters = {
            url: url,
            element: targetId
        };
        exchange.loadPage(parameters);
    };
    var setParametersPost = function (url, form, antiToken){
        var parameters = {
            url: url,
            data: $(form).serialize(),
            type:'POST',
            element: element,
            headers: {
                'RequestVerificationToken': antiToken
            }
        };
        exchange.loadPage(parameters);
    };
    
   
    return {
        
        exchangeFormSubmit: function (url){            
            var r = confirm("Are you sure you?\nEither OK or Cancel.");
            if (r === true) {
                setParametersPost(url, "#exchangeForm");
            }
        },
        wantToBuy: function (url){
            setParametersGet(url);
        },
         loginForm : function(url){
            setParametersGet(url);
        },
        registerForm: function (url) {
            setParametersGet(url);
        },
        submitLoginForm: function (url, antiToken){
            setParametersPost(url, "#loginForm", antiToken);
        },
        submitRegisterForm: function (url, antiToken) {
            setParametersPost(url, "#registerForm", antiToken);
        },
        submitMyCurrenciesForm: function (url, antiToken){
            setParametersPost(url, "#currenciesForm", antiToken);
        },
        changePage: function (url, targetId){
            setParametersGet(url, targetId);
        },
        submitFunc: function (url, form, antiToken) {
            form = "#" + form;
            setParametersPost(url, form, antiToken);
        }



    };
   //exchange.loadPage({url:"hello"});
   // console.log(exchange);
}); 

//(function() { require('./home/home').loginForm('<?= $this->url('home',array('action' => 'login'));?>') })()