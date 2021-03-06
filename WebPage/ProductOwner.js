const baseuri = "https://3semester-denroedegruppe.azurewebsites.net/api/parkinglots"
const uriCarcolor = "https://3semester-denroedegruppe.azurewebsites.net/api/cars/CarColor"

Vue.createApp({
    data() {
        return {
            singleCount: null,
            day: 0,
            month: 0,
            year: 0,
            SingleColor: null,
            gennemsnit: 0,
            ParkingLots: []

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
        async GetCarColor() {
            Color = document.getElementById("cars").value
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
        },
        async AverageCarTime(){
            const response = await axios.get(baseuri)
            this.ParkingLots = response.data
            sum = 0;
            this.ParkingLots.forEach ((item) =>
            {
                sum += item.parkingTime
            }
            )
            this.gennemsnit = sum/this.ParkingLots.length
            this.gennemsnit = +this.gennemsnit.toFixed(2);
        }

    }
}).mount("#app")
