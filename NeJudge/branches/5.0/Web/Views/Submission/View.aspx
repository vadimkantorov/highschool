<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Model.ProgramSource>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>Печать</title>
	<link href="/content/highlighter/vs.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="/content/highlighter/salagaev.js"></script>
	<script type="text/javascript">
		hljs.initHighlightingOnLoad();
	</script>
</head>
<body>
	<div class="watermark top right"><div><img alt="" src="watermark.png" /></div></div>
	<div class="content">
	<pre><code><%=Model.Code %></code></pre>
	</div>
</body>
</html>
