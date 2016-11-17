var HomeEventFacultyManage = new Vue({
    el: '#app',
    data: {
        times:  ['12am', '1am', '2am', '3am', '4am', '5am', '6am', '7am', '8am', '9am', '10am', '11am', '12pm', '1pm', '2pm', '3pm', '4pm', '5pm', '6pm', '7pm', '8pm', '9pm', '10pm', '11pm' ],
        checkIn: '',
        checkOut: '',
        bookHotel: false,
        travelMethod: '',
        departureDate: '',
        returnDate: '',
        departureLocation: '',
        departureTime: '',
        returnLocation: '',
        returnTime: ''
    },
    computed: {
        hotelCheck: function(){
            if (this.bookHotel) {
                return true;
            } else {
                return false;
            }
        },
        travelCheck: function(){
            if (this.travelMethod == 'Please book my air travel' || this.travelMethod == 'Please book my train travel') {
                return true;
            } else{
                return false;
            }
        },

        validForm: function () {
            var valid = 0;

            this.travelMethod.trim() == '' ? valid++ : null;

            if (this.travelCheck) {
                this.departureDate.trim() == '' ? valid++ : null;
                this.returnDate.trim() == '' ? valid++ : null;
                this.departureLocation.trim() == '' ? valid++ : null;
                this.returnLocation.trim() == '' ? valid++ : null;
                this.departureTime.trim() == '' ? valid++ : null;
                this.returnTime.trim() == '' ? valid++ : null;
            }
            
            if (this.bookHotel !== true && this.bookHotel !== false) {
                valid++;
            }

            if (this.hotelCheck == true) {
                this.checkIn.trim() == '' ? valid++ : null;
                this.checkOut.trim() == '' ? valid++ : null;
            }

            return valid == 0;
        }
    }
})