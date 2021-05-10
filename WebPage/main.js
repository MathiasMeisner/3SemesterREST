const baseUri = "https://localhost:44350/api/bookings"

Vue.createApp({
    data() {
        return {
            bookings: [],
            oneBooking: null,
            error: null,
            id: ""
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
            if (uid == null || uid =="") {
                this.error = "No user id"
                this.bookings =[]
            } else{
                const uri = baseUri + "/" + uid
                console.log(uri)
                const response = await axios.get(uri)
                this.oneBooking = response.data
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
        }
    }
}).mount("#app")