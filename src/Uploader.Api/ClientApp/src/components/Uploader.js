import React, { Component } from 'react';
import Axios from "axios";

export class Uploader extends Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedFile: null
        };
    }
    
    upload = () => {
        const data = new FormData()
        data.append('file', this.state.selectedFile)
        Axios.post("https://localhost:5001/Transaction/UploadFile", data, {
        }).then(res => { // then print response status
            alert("Success")
        }).catch(err => {
            alert(err.response.data.errorMessage);
        })
    }
    
    onChangeHandler = (e) => {
        this.setState({
            selectedFile: e.target.files[0]
        })
    }
    
    render() {
        return (
            <div>
                <h1>Uploader</h1>

                <input type="file" name="file" onChange={this.onChangeHandler}/>
                <button className="btn btn-primary" onClick={this.upload}>UPLOAD</button>
            </div>
        );
    }
}
