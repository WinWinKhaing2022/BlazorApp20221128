using BlazorWASM20221128.Shared.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace BlazorWASM20221128.Client.Pages
{
    public partial class BlogList
    {
        [Inject]
        public HttpClient Http { get; set; }

        public BlogModel[] lstBlog { get; set; }

        public BlogModel reqModel { get; set; } = new BlogModel();

        protected override async Task OnInitializedAsync()
        {
            await List();
        }

        async Task List()
        {
            lstBlog = await Http.GetFromJsonAsync<BlogModel[]>("api/Blog/1/10");
        }

        async Task Save()
        {
            //HttpContent content = new StringContent(JsonConvert.SerializeObject(reqModel), Encoding.UTF8, "application/json");
            var response = await Http.PostAsJsonAsync("api/Blog", reqModel);
            await List();
        }
    }
}
