const bookingUri = "https://localhost:44350/api/bookings"
const parkingBoothUri = "https://localhost:44350/api/parkingbooths"
const parkingLotUri = "https://localhost:44350/api/parkinglots"

Vue.createApp({
    data() {
        return {
            parkingBooths: [],
            parkingBooth: null,
            parkingBooth: { isBooked: "", isOccupied: "" },
            bookings: [],
            Booking: null,
            Booking: { username: "", parkingId: "", licensePlate: "", startTime: "", endTime: "" },
            Addbooking: "",
            error: null,
            newBookingError: null,
            id: "",
            LicensePlate: "",
            SingleLicensePlate: null,
            parkingLots: [],
            emptyLots: 0

        }

    },
    async created() {
        console.log("created method called")
        await this.helperGetBookingsPosts(bookingUri)
        await this.helperGetParkingBoothsPosts(parkingBoothUri)
        await this.helperGetParkingLotsPosts(parkingLotUri)
    },

    methods: {
        async getByUserId(uid) {
            if (uid == null || uid == "") {
                this.error = "No user id"
                this.bookings = []
                console.log(uid)
            } else {
                const uri = bookingUri + "/" + uid
                console.log(uri)
                const response = await axios.get(uri)
                this.Booking = response.data
                this.helperGetBookingsPosts(uri)
            }
            console.log(this.bookings)
        },
        async getByParkingBoothId(uid) {
            if (uid == null || uid == "") {
                this.error = "No parkingBooth id"
                this.parkingBooths = []
                console.log(uid)
            } else {
                const uri = parkingBoothUri + "/" + uid
                console.log(uri)
                const response = await axios.get(uri)
                this.parkingBooth = response.data
                this.helperGetBookingsPosts(uri)
            }
            console.log(this.parkingBooths)
        },

        async getByLicensePlate(LicensePlate){
            const url = bookingUri + "/LicensePlate/" + LicensePlate
            try{
                response = await axios.get(url)
                this.SingleLicensePlate = await response.data
            } catch (ex) {
                alert (ex.message)
            }
        },

        async helperGetBookingsPosts(uri) {
            try {
                const response = await axios.get(uri)
                this.bookings = await response.data
                this.error = null
            } catch (ex) {
                this.bookings = []
                this.error = ex.message
                console.log(ex)
            }
        },
        async helperGetParkingBoothsPosts(uri) {
            try {
                const response = await axios.get(uri)
                this.parkingBooths = await response.data
                this.error = null
            } catch (ex) {
                this.parkingBooths = []
                this.error = ex.message
                console.log(ex)
            }
        },
        async helperGetParkingLotsPosts(uri){
            try{
                const response = await axios.get(uri)
                this.parkingLots = await response.data
                amountOfLots = 0
                for(i = 0; i < this.parkingLots.length; i++){
                    if(this.parkingLots[i].isin == 1){
                        amountOfLots +=1
                    }
                }
                this.emptyLots = 30 - amountOfLots

            } catch(ex){
                console.log(ex)
            }
        },

        getAllBookings(){
            this.helperGetBookingsPosts(bookingUri)
        },

        tryAddBooking(){
            if (this.Booking.endTime - this.Booking.startTime < 12) {
               this.AddBooking() 
            }
            else{
                this.newBookingError = "du kunne ikke tilføje denne booking"
                console.log("du kunne ikke tilføje denne booking")
            }
        },

        async AddBooking() {
            try {
                response = await axios.post(bookingUri, this.Booking)
                this.Addbooking = "response" + response.status + " " + response.statusText
                console.log(this.Booking)
                //console.log(Booking.id)
                this.getAllBookings()
                this.notifyMe()
            }
            catch (ex) {
                console.log(ex.message)
                alert(ex.message)
            }
        },
        notifyMe() {
            NotificationMessage = "Du valgt parkingsplads nummer " + this.Booking.parkingId + 
            " Du har valgt start tid til: " + this.Booking.startTime + 
            " Din parkingplads udløber klokken: " + this.Booking.endTime
            console.log(NotificationMessage)
            // Let's check if the browser supports notifications
            if (!("Notification" in window)) {
              alert("This browser does not support desktop notification");
            }
          
            // Let's check whether notification permissions have already been granted
            else if (Notification.permission === "granted") {
              // If it's okay let's create a notification
              var notification = new Notification(NotificationMessage);
            }
          
            // Otherwise, we need to ask the user for permission
            else if (Notification.permission !== "denied") {
              Notification.requestPermission().then(function (permission) {
                // If the user accepts, let's create a notification
                if (permission === "granted") {
                  var notification = new Notification(NotificationMessage);
                }
              });
            }
          
            // At last, if the user has denied notifications, and you
            // want to be respectful there is no need to bother them any more.
          }
    }
}).mount("#app")