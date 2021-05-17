const bookingUri = "https://localhost:44350/api/bookings"
const parkingBoothUri = "https://localhost:44350/api/parkingbooths"


Vue.createApp({
    data() {
        return {
            parkingBooths: [],
            parkingBooth: null,
            parkingBooth: { isBooked: "", isOccupied: "", parkinglotId: "" },
            bookings: [],
            Booking: null,
            Booking: { username: "", licensePlate: "", bookTime: "" },
            Addbooking: "",
            error: null,
            id: "",
            LicensePlate: "",
            SingleLicensePlate: null,
        }

    },
    async created() {
        console.log("created method called")
        this.helperGetPosts(bookingUri)
        this.helperGetPosts(parkingBoothUri)
    },
    methods: {
        resetList() {
            this.bookings = [],
                this.error = null
            this.Booking = null
        },
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
                this.helperGetPosts(uri)
            }
            console.log(this.bookings)
        },
        async getByParkingBoothId(uid) {
            if (uid == null || uid == "") {
                this.error = "No user id"
                this.parkingBooths = []
                console.log(uid)
            } else {
                const uri = parkingBoothUri + "/" + uid
                console.log(uri)
                const response = await axios.get(uri)
                this.parkingBooth = response.data
                this.helperGetPosts(uri)
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

        async helperGetPosts(uri) {
            try {
                const response = await axios.get(uri)
                this.bookings = await response.data
                this.parkingBooths = await response.data
                this.error = null
            } catch (ex) {
                this.bookings = []
                this.parkingBooths = []
                this.error = ex.message
                console.log(ex)
            }
        },

        getAllBookings(){
            this.helperGetPosts(bookingUri)
        },

        async AddBooking() {
            try {
                response = await axios.post(bookingUri, this.Booking)
                this.Addbooking = "response" + response.status + " " + response.statusText
                console.log(this.Booking)
                //console.log(Booking.id)
                this.getAllBookings()
            }
            catch (ex) {
                console.log(ex.message)
                alert(ex.message)
            }
        },
    }
}).mount("#app")