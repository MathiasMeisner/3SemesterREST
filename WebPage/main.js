const bookingUri = "https://3semester-denroedegruppe.azurewebsites.net/api/bookings"
const parkingBoothUri = "https://3semester-denroedegruppe.azurewebsites.net/api/parkingbooths"
const parkingLotUri = "https://3semester-denroedegruppe.azurewebsites.net/api/parkinglots"

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
            newBookingSuccess: null,
            id: "",
            LicensePlate: "",
            SingleLicensePlate: null,
            parkingLots: [],
            emptyLots: 0,
            WarningTime: 0,
            reservationCode: ""
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
                this.error = "No username"
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

        async getByLicensePlate(LicensePlate) {
            const url = bookingUri + "/LicensePlate/" + LicensePlate
            try {
                response = await axios.get(url)
                this.SingleLicensePlate = await response.data
            } catch (ex) {
                alert(ex.message)
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
        async helperGetParkingLotsPosts(uri) {
            try {
                const response = await axios.get(uri)
                this.parkingLots = await response.data
                amountOfLots = 0
                for (i = 0; i < this.parkingLots.length; i++) {
                    if (this.parkingLots[i].isin == 1) {
                        amountOfLots += 1
                    }
                }
                this.emptyLots = 30 - amountOfLots

            } catch (ex) {
                console.log(ex)
            }
        },

        getAllBookings() {
            this.helperGetBookingsPosts(bookingUri)
        },

        makeReservationCode(length) {
            var result = [];
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890123456789";
            var charactersLength = characters.length;
            for (var i = 0; i < length; i++) {
                result.push(characters.charAt(Math.floor(Math.random() * charactersLength)));
            }
            this.reservationCode = result.join("");

        },

        tryAddBooking() {
            var StartDatetime = this.Booking.startTime
            var EndDateTime = this.Booking.endTime
            forskel = new Date(EndDateTime).getTime() - new Date(StartDatetime).getTime()
            if (forskel < 43200000) {
                this.makeReservationCode(6)
                console.log(this.reservationCode);
                this.AddBooking()
                this.newBookingSuccess = "Booking oprettet!"
            }
            else {
                this.newBookingError = "Du m?? ikke booke i l??ngere end 12 timer"
                console.log("Du m?? ikke booke i l??ngere end 12 timer")
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
                WarningTimeinmils = this.WarningTime * 60000
                this.sleep(WarningTimeinmils).then(() => {
                    this.notify()
                });
            }
            catch (ex) {
                console.log(ex.message)
                alert(ex.message)
            }
        },
        notifyMe() {
            NotificationMessage = "Du valgt parkingsplads nummer " + this.Booking.parkingId +
                " Du har valgt start tid til: " + this.Booking.startTime +
                " Din parkingplads udl??ber klokken: " + this.Booking.endTime
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
        },

        notify() {

            NotificationMessage = "din parkingsplads nummer er: " + this.Booking.parkingId +
                " den udl??ber klokken: " + this.Booking.endTime

            if (!("Notification" in window)) {
                alert("This browser does not support desktop notification");
            }

            // Let's check whether notification permissions have already been granted
            else if (Notification.permission === "granted") {
                // If it's okay let's create a notification
                var notification = new Notification(NotificationMessage);
            }

            else if (Notification.permission !== "denied") {
                Notification.requestPermission().then(function (permission) {
                    // If the user accepts, let's create a notification
                    if (permission === "granted") {
                        var notification = new Notification(NotificationMessage);
                    }
                });
            }
        },
        sleep (time) {
            return new Promise((resolve) => setTimeout(resolve, time));
          }
    }
}).mount("#app")