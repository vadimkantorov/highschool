<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Tuple<string, Model.User>>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Пользователи созданы</title>
	</head>
	<body>
		<table>
			<tr>
				<th>Id</th>
				<th>Отображаемое имя</th>
				<th>Имя пользователя</th>
				<th>Пароль</th>
			</tr>
			<%foreach(var userLine in Model) {%>
				<tr>
					<td><%=userLine.Item2.Id %></td>
					<td><%=userLine.Item2.DisplayName %></td>
					<td><%=userLine.Item2.UserName %></td>
					<td><%=userLine.Item1 %></td>
				</tr>
			<%} %>
		</table>
	</body>
</html>