const baseUri = "https://localhost:44350/api/bookings"

Vue.createApp({
    data() {
        return {
            bookings: [],
            oneBooking: null,
            error: null,
            id: "",
            Booking: { username: "", licensePlate: "", bookTime: "" },
            Addbooking: "",
        }

    },
    async created() {
        console.log("created method called")
        this.helperGetPosts(baseUri)
    },
    methods: {
        resetList() {
            this.bookings = [],
                this.error = null
            this.oneBooking = null
        },
        async getByUserId(uid) {
            if (uid == null || uid == "") {
                this.error = "No user id"
                this.bookings = []
            } else {
                const uri = baseUri + "/" + uid
                console.log(uri)
                const response = await axios.get(uri)
                this.oneBooking = response.data
                this.helperGetPosts(uri)
            }
            console.log(this.bookings)
        },
        async helperGetPosts(uri) {
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
        async AddBooking() {
            try {
                response = await axios.post(baseUri, this.Booking)
                this.Addbooking = "response" + response.status + " " + response.statusText
                console.log(this.Addbooking)
                //console.log(Booking.id)
                this.getByUserId(this.Booking.id)
            }
            catch (ex) {
                console.log(ex.message)
                alert(ex.message)
            }
        },
    }
}).mount("#app")