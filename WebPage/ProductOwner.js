const baseuri = "https://localhost:44350/api/parkinglots"
const uriCarcolor = "https://localhost:44350/api/cars/CarColor"

Vue.createApp({
    data() {
        return {
            singleCount: null,
            day: 0,
            month: 0,
            year: 0,
            SingleColor: null

        }
    },
    methods: {
        async CountByDay(day, month, year) {
            const newuri = baseuri + "/" + year + "/" + month + "/" + day
            try {
                const response = await axios.get(newuri)
                this.singleCount = await response.data
                if (this.singleCount == 0) {
                    this.singleCount = "There is no data for this day "
                }
            }
            catch (ex) {
                alert(ex.message)
            }
        },
        async GetCarColor(Color) {
            const newuri = uriCarcolor + "/" + Color
            try {
                const response = await axios.get(newuri)
                this.SingleColor = await response.data
                if (this.SingleColor == 0) {
                    this.SingleColor = "Det er ingen biler med denne ønskede farve: " + Color
                }
            }
            catch (ex) {
                alert(ex.message)
            }
        }

    }
}).mount("#app")
