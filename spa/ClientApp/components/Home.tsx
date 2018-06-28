import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Home extends React.Component<RouteComponentProps<{}>, FormDataDetails> {
    constructor(props) {
        super(props);
        this.state = { name: "", lastName: "", birthDate :""};
    }

    onSubmit = (event) => {

        fetch('api/postFormData',
            {
                method: 'post',
                body: JSON.stringify(this.state),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });
        this.setState({ name: "", lastName: "", birthDate: ""});
        event.preventDefault();
    }

    onChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    public render() {
        return <div>
            <h1>Contact form</h1>
            <form onSubmit={this.onSubmit}>
                       <label>
                           Name:
                           <input type="text" name="name" value={this.state.name} onChange={this.onChange} />
                       </label>
                       <label>
                           Last name:
                           <input type="text" name="lastName" value={this.state.lastName} onChange={this.onChange}/>
                       </label>
                       <label>
                           Birthday:
                           <input type="date" name="birthDate" value={this.state.birthDate} onChange={this.onChange} />
                       </label>

                       <input type="submit" value="Submit" />
                   </form>
        </div>;
    }
}

interface FormDataDetails {
    name: string;
    lastName: string;
    birthDate: string;
}
