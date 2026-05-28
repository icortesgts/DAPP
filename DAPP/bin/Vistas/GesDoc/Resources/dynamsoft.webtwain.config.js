//
// Dynamsoft JavaScript Library for Basic Initiation of Dynamic Web TWAIN
// More info on DWT: http://www.dynamsoft.com/Products/WebTWAIN_Overview.aspx
//
// Copyright 2015, Dynamsoft Corporation 
// Author: Dynamsoft Team
// Version: 11.2
//
/// <reference path="dynamsoft.webtwain.initiate.js" />
var Dynamsoft = Dynamsoft || { WebTwainEnv: {} };

Dynamsoft.WebTwainEnv.AutoLoad = true;
///
Dynamsoft.WebTwainEnv.Containers = [{ContainerId:'dwtcontrolContainer', Width:270, Height:350}];
///
Dynamsoft.WebTwainEnv.ProductKey = '2464B22B5438C142FC68B8206B813F7F974C5F6436556DCFC27949B71DE100B2974C5F6436556DCFB314571056836708974C5F6436556DCF9D25AADCCAFAA308974C5F6436556DCF27194FEAB0BF1B12974C5F6436556DCF696FDEBFCE350907974C5F6436556DCF32D00E47C8FB9A5D974C5F6436556DCFF9390FCDF8E9D345974C5F6436556DCFDE36335D302A0AFE974C5F6436556DCF02DD1D6834E8E0E4974C5F6436556DCF889987C71B903FB8974C5F6436556DCF8EE676B8EA9E2A20974C5F6436556DCF2C6724F0575A13E5974C5F6436556DCFACA38AAFE35594EC974C5F6436556DCF04C8FE295359843EE0000000';
///
Dynamsoft.WebTwainEnv.Trial = true;
///
Dynamsoft.WebTwainEnv.ActiveXInstallWithCAB = false;
///
Dynamsoft.WebTwainEnv.Debug = false; // only for debugger output
///
// Dynamsoft.WebTwainEnv.ResourcesPath = 'Resources';

/// All callbacks are defined in the dynamsoft.webtwain.install.js file, you can customize them.

// Dynamsoft.WebTwainEnv.RegisterEvent('OnWebTwainReady', function(){
// 		// webtwain has been inited
// });

