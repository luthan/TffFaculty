var HomeIndexAdmin = new Vue({
    name: "main",
    el: '#app',
    data: {
        events: [],
        searchQuery: '',
        status: ''
    },
    computed: {
        filteredEvents: function () {
            var self = this;
            if (self.status == '') {
                return self.events.filter(function (f) {
                    var searchRegex = new RegExp(self.searchQuery, 'i');
                    return (searchRegex.test(f.EventCode) || searchRegex.test(f.EventName) || searchRegex.test(f.Type));
                })
            } else {
                return self.events.filter(function (f) {
                    var searchRegex = new RegExp(self.searchQuery, 'i');
                    return (searchRegex.test(f.EventCode) || searchRegex.test(f.EventName) || searchRegex.test(f.Type)) && f.Status == self.status;
                })
            }
        }
    },
    created: function () {
        var self = this;
        axios.get("/Api/GetEventsForAdmin/"+userId)
        .then(function (response) {
            console.log(response);
            if (response.status == 200) {
                self.events = response.data;
                console.log(response.data);
            } else {
                alert("Something went wrong");
            }
        });
    }
})

Vue.filter('date', function (value) {
    return moment(value).format('MM/DD/YYYY');
})
