using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TodoApp.Models
{
    public class TodoRepositoryJson : ITodoRepository
    {
        private readonly string _filePath;
        private static List<Todo> _todos = new List<Todo>();

        //public TodoRepositoryFile()
        //{
        //    _todos = new List<Todo>
        //    {
        //        new Todo { Id = 1, Title = "ASP.NET Core 학습", IsDone = false },
        //        new Todo { Id = 2, Title = "Blazor 학습", IsDone = false },
        //        new Todo { Id = 3, Title = "C# 학습", IsDone = true },
        //    };
        //}

        public TodoRepositoryJson(string filePath = @"G:\Study\Todos.json")
        {
            this._filePath = filePath;
            var todos = File.ReadAllText(filePath, Encoding.Default);
            //Json 패키지 이용하여 읽기
            _todos = JsonConvert.DeserializeObject<List<Todo>>(todos);
        }

        // 인-메모리 데이터베이스 사용 영역
        public void Add(Todo model)
        {
            model.Id = _todos.Max(t => t.Id) + 1;
            _todos.Add(model);

            //Json 파일 저장
            string json = JsonConvert.SerializeObject(_todos, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public List<Todo> GetAll()
        {
            return _todos;
        }
    }
}
