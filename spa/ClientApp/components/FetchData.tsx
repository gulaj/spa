import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface WorkerDataDetailsState {
    worker: WorkerDataDetails;
    loading: boolean;
    isExists : boolean;
}

export class FetchData extends React.Component<RouteComponentProps<{}>, WorkerDataDetailsState > {
    constructor() {
        super();
        this.state = { worker: { name: "", lastName: "", birthDate: "" }, loading: true, isExists: false };

        fetch('api/workerDetails')
            .then(response => response.json() as Promise<WorkerDataDetails>)
            .then(data => {
                let isUserExists = data.name != null;
                this.setState({ worker: data, loading: false, isExists: isUserExists });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderWorkerDetails(this.state.worker);

        return <div>
            <h1>Worker data</h1>
            { contents }
        </div>;
    }

    private static renderWorkerDetails(worker: WorkerDataDetails) {
        if (worker.name == null) {
            return <div>
                <p>There is no data to display</p>
                   </div>
        }

        return <div>
            <p>Name: {worker.name}</p>
            <p>Lastname: {worker.lastName}</p>
            <p>Birth Date: {worker.birthDate}</p>
            </div>;
    }
}

interface WorkerDataDetails {
    name: string;
    lastName: string;
    birthDate: string;
}
