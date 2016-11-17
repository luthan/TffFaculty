var AdminFaculty = new Vue({
    name: "main",
    el: '#app',
    data:{
        faculty: [],
        searchQuery: '',
        test: '',
        status: ''
    },
    computed: {
        filteredUsers: function () {
            var self = this;
            if (self.status == '') {
                return self.faculty.filter(function (f) {
                    var searchRegex = new RegExp(self.searchQuery, 'i');
                    return (searchRegex.test(f.FirstName) || searchRegex.test(f.LastName) || searchRegex.test(f.Specialty));
                })
            } else {
                return self.faculty.filter(function (f) {
                    var searchRegex = new RegExp(self.searchQuery, 'i');
                    return (searchRegex.test(f.FirstName) || searchRegex.test(f.LastName) || searchRegex.test(f.Specialty)) && f.Status == self.status;
                })
            }          
        }
    },
    created: function () {
        var self = this;
        axios.get("/Api/GetFaculty")
        .then(function (response) {
            if (response.status == 200) {
                self.faculty = response.data;
                console.log(response.data);
            } else {
                alert("Something went wrong");
            }
        });
    }
})
