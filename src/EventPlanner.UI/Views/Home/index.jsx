import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import 'bootstrap/dist/css/bootstrap.css';

const baseUrl = 'http://localhost:13692/';

var List = React.createClass({
    render: function() {
        var dict = this.props.data;
        return (<div>
        {  Object.getOwnPropertyNames(dict).map(function(item) {
            return <div>{dict[item]}</div>
        })
        }
        </div>);
    }
});


class Layout extends React.Component {

    constructor()
    {
        super();
        this.state = {choices: []};
    }

    doStuff(e) { 
        axios
            .get(`${baseUrl}/api/dashboard`)
            .then(response => {
                this.setState({
                    choices: response.data.choices
                });
            });
    }
    render() {
        return (
            <div>
                <button className="btn btn-danger" onClick={this.doStuff.bind(this)}>Click</button>
                <h1><List data={this.state.choices}/></h1>
            </div>
        );
                }
}

const app = document.getElementById('app');
ReactDOM.render(<Layout/>, app); 