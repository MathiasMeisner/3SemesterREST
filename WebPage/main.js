const baseUri = "http://jsonplaceholder.typicode.com/posts"

Vue.createApp({
    data() {
        return {
            posts: [],
            error: null,
            userId: ""
        }

    },
    async created() {
        console.log("created method called")
        this.helperGetPosts(baseUri)
    },
    methods: {
        resetList() {
            this.posts = [],
            this.error = null
        },
        async getByUserId(uid) {
            if (uid == null || uid =="") {
                this.error = "No user id"
                this.posts =[]
            } else{
                const uri = baseUri + "?userId=" + uid
                console.log("getByUserId" + uri)
                this.helperGetPosts(uri)
            }
        },
        async helperGetPosts(uri) {
            try {
                const response = await axios.get(uri)
                this.posts = await response.data
                this.error = null
            } catch (ex) {
                this.posts = []
                this.error = ex.message
            }
        }
    }
}).mount("#app")