const baseuri = "https://localhost:44350/api/parkinglots"

Vue.createApp({
    data() {
        return {
            singleCount: null,
            day: 0,
            month: 0,
            year: 0

        }
    },
    methods: {
        async CountByDay(day, month, year) {
            const newuri = baseuri + "/" + year + "/" + month + "/" + day
            try {
                const response = await axios.get(newuri)
                this.singleCount = await response.data
                if(this.singleCount == 0){
                    this.singleCount = "There is no data for this day "
                }
            }
            catch (ex) {
                alert(ex.message)
            }
        }
    }
}).mount("#app")
