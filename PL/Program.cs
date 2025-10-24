using lab_3.BLL;
using lab_3.Presentation;

var studentService = new StudentService();
var menu = new Menu(studentService);
menu.Run();
