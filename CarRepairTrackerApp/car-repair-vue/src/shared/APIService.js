import axios from 'axios';
import auth from "@/shared/auth";

export class APIService {
    constructor() {
    }

    async login(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/login`;
        let response;

        try {
            response = await axios.post(url, data);
        } catch (error) {
            if (error.response.status != 200) {
                throw "Your username and/or password is invalid";
            }
        }
        
        
        return response.data;
    }

    async register(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/register`;
        let res = await axios.post(url, data);
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }

    async registerEmployee(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/register/employee`;
        let res = await axios.post(url, data, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        });
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }

    getUserById(userId) {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/user/${userId}`;
        return axios.get(url, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        }).then(response => response.data);
    }

    async postIncident(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident/new`;
        let res = await axios.post(url, data, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        });
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }

    async putPayIncident(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident/paid`;
        let res = await axios.put(url, data, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        });
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }

    async markIncidentComplete(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident/complete`;
        let res = await axios.put(url, data, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        });
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }

    getUserIncidents() {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident/${auth.getUser().sub}`;
        return axios.get(url, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        }).then(response => response.data);
    }

    getIncidents() {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident`;
        return axios.get(url, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        }).then(response => response.data);
    }

    getRepairLines(incidentId) {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident/repairlines/${incidentId}`;
        return axios.get(url, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        }).then(response => response.data);
    }

    async addRepairLine(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident/line/add`;
        let res = await axios.post(url, data, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        });
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }

    async approveRepairLine(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident/line/approve`;
        let res = await axios.put(url, data, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        });
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }

    async declineRepairLine(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/incident/line/decline`;
        let res = await axios.put(url, data, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        });
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }
}