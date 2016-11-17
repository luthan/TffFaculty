var AdminCreateEvent = new Vue({
    el: '#app',
    data: {
        states: ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware', 'District of Columbia', 'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'],
        type: '',
        venueName: '',
        address1: '',
        city: '',
        state: '',
        zip: '',
        hotelAllowance: '',
        airfareAllowance: '',
        groundTransportAllowance: ''
    },
    computed: {
        validForm: function () {
            var valid = 0;

            if (this.type != 'Enduring') {
                this.venueName.trim() == '' ? valid++ : null;
                this.address1.trim() == '' ? valid++ : null;
                this.city.trim() == '' ? valid++ : null;
                this.state.trim() == '' ? valid++ : null;
                this.zip.trim() == '' ? valid++ : null;
                this.hotelAllowance.trim() == '' ? valid++ : null;
                this.airfareAllowance.trim() == '' ? valid++ : null;
                this.groundTransportAllowance.trim() == '' ? valid++ : null;
            }

            return valid == 0;
        }
    }
})